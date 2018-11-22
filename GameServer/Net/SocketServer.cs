using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using GameServer.GameServer;
using GameServer.Models;
using GameServer.Utils;
using NetcodeIO.NET;
using ReliableNetcode;

namespace GameServer
{
    public delegate void ClientConnected(RemoteClient remoteClient);
    public delegate void ClientDisconnect(RemoteClient remoteClient);
    public delegate void DataReceived(RemoteClient client, byte[] payload, int payloadSize);
    
    public class SocketServer
    {
        private const int HEADER_OFFSET = 2;
  
        public ClientConnected OnClientConnected;
        public ClientDisconnect OnClientDisconnected;
        public DataReceived OnDataReceived;
        
        static readonly byte[] _privateKey = new byte[]
        {
            0x60, 0x6a, 0xbe, 0x6e, 0xc9, 0x19, 0x10, 0xea,
            0x9a, 0x65, 0x62, 0xf6, 0x6f, 0x2b, 0x30, 0xe4,
            0x43, 0x71, 0xd6, 0x2c, 0xd1, 0x99, 0x27, 0x26,
            0x6b, 0x3c, 0x60, 0xf4, 0xb7, 0x15, 0xab, 0xa1
        };

        private Server _server;
        private readonly string _ipAddress;
        private readonly int _port;
        private readonly int _maxClients;
        
        private readonly ConcurrentDictionary<RemoteClient, ReliableEndpoint> remoteClient_list;
        
        public SocketServer(string ipAddress, int port = 8559, int maxClients = 5)
        {
            remoteClient_list = new ConcurrentDictionary<RemoteClient, ReliableEndpoint>();    
        }

        public void StartServer()
        {
            //var privateKeyBytes = Encoding.ASCII.GetBytes("thisismysupersecretkeyimusing123");
            
            _server = new Server(
                5,		            // int maximum number of clients which can connect to this server at one time
                "127.0.0.1", 8559,	// string public address and int port clients will connect to
                1UL,		        // ulong protocol ID shared between clients and server
                _privateKey		    // byte[32] private crypto key shared between backend servers
            );
            
            // Called when a client has connected
            _server.OnClientConnected += ClientConnectedHandler;		// void( RemoteClient client )

            // Called when a client disconnects
            _server.OnClientDisconnected += ClientDisconnectedHandler;	// void( RemoteClient client )

            // Called when a payload has been received from a client
            // Note that you should not keep a reference to the payload, as it will be returned to a pool after this call completes.
            _server.OnClientMessageReceived += ClientMessageReceivedHandler;	// void( RemoteClient client, byte[] payload, int payloadSize )
            
            _server.Start(); 
            Console.WriteLine("Waiting for connections...");
        }

        public void StopServer(int secondsToStop = 0)
        {
            if (secondsToStop == 0)
            {
                foreach (var (remoteClient, reliableEndpoint) in remoteClient_list)
                {
                    Send(remoteClient, new byte[256], 256, QosType.Reliable);
                    Task.Delay(1000).ContinueWith(t => _server.Disconnect(remoteClient));
                }

                _server.Stop();
            }
        }

        public void DisconnectClient(RemoteClient remoteClient, string reason)
        {
            Send(remoteClient, new byte[256], 256, QosType.Reliable);
            Task.Delay(1000).ContinueWith(t => _server.Disconnect(remoteClient));
        }
                
        public void SendAll<T>(T data, MessageType type, QosType qosType = QosType.Unreliable)
        {        
            var objData = StructTools.RawSerialize(data);
            var objType = BitConverter.GetBytes((short)type);
            var payload = objType.Concat(objData).ToArray();
            
            foreach (var (remoteClient, remoteEndpoint) in remoteClient_list)
            {
                remoteEndpoint.TransmitCallback = ( buffer, size ) => {  remoteClient.SendPayload(buffer, size); };               
                remoteEndpoint.SendMessage(payload, payload.Length, qosType);
            }
        }
        
        public void Send<T>(RemoteClient client, T data, MessageType type, QosType qosType = QosType.Unreliable)
        {        
            var objData = StructTools.RawSerialize(data);
            var objType = BitConverter.GetBytes((short)type);
            var payload = objType.Concat(objData).ToArray();
            remoteClient_list.TryGetValue(client, out var reliableEndpoint);
            reliableEndpoint?.SendMessage(payload, payload.Length, qosType);
        }
        
        private void ClientConnectedHandler(RemoteClient client)
        {     
            Console.WriteLine($"[{DateTime.Now}]: Server: Client Connected: id:{client.ClientID}");
            ReliableEndpoint _reliableEndpoint = new ReliableEndpoint();
            _reliableEndpoint.ReceiveCallback += ReliableClientMessageReceived;
            if (remoteClient_list.TryAdd(client, _reliableEndpoint))
            {
                OnClientConnected?.Invoke(client);
            }
            
        }
        
        private void ClientDisconnectedHandler(RemoteClient remoteClient)
        {    
            Console.WriteLine($"[{DateTime.Now}]: Server: Client Disconnected: id:{remoteClient.ClientID}");
            remoteClient_list.TryRemove(remoteClient, out _);
            OnClientDisconnected?.Invoke(remoteClient);
            
        }
        
        private void ClientMessageReceivedHandler(RemoteClient remoteClient, byte[] payload, int payloadSize)
        {
            Console.WriteLine($"[{DateTime.Now}]: Server: Client Data Received: id:{remoteClient.ClientID}");
            remoteClient_list.TryGetValue(remoteClient, out var _reliableEndpoint);
            _reliableEndpoint.ReceiveCallback = ( buffer, size ) =>
            {                 
                OnDataReceived?.Invoke(remoteClient, buffer, size);              
            };
            
        }
            
        private void ReliableClientMessageReceived(byte[] payload, int payloadSize)
        {
            
            Console.WriteLine($"Received Payload of {payloadSize} bytes.");
            MessageType type = (MessageType)BitConverter.ToInt16(payload, 0);
            
            if (type == MessageType.Chat)
            {
                //var data = StructTools.RawDeserialize<ChatMessage>(payload, HEADER_OFFSET);
                //OnChatMessage?.Invoke(data);
                var chatMessage = StructTools.RawDeserialize<ChatMessage>(payload, HEADER_OFFSET);
                //OnChatMessageReceived?.Invoke(chatMessage);
                SendAll(chatMessage, MessageType.Chat);
            }
            else
            {
                Console.WriteLine($"Type: {(MessageType) Enum.Parse(typeof(MessageType), type.ToString())}");

                var pos = StructTools.RawDeserialize<Position>(payload, HEADER_OFFSET); // 0 is offset in byte[]
                //Console.WriteLine($"messageReceivedHandler: {client} sent {payloadSize} bytes of data.");
                Console.WriteLine(pos.ToString());
                SendAll(payload, payloadSize);
            }

        }    
        
        public void Send(RemoteClient client, byte[] payload, int payloadSize, QosType type = QosType.Unreliable)
        {
            remoteClient_list.TryGetValue(client, out var reliableEndpoint);
            reliableEndpoint?.SendMessage(payload, payloadSize, type);
        }
        
        public void SendAll(byte[] payload, int payloadSize, QosType type = QosType.Unreliable)
        {        
            //Console.WriteLine($"Sending Payload of {payloadSize} bytes.");
            foreach (var (remoteClient, reliableEndpoint) in remoteClient_list)
            {
                reliableEndpoint.TransmitCallback = ( buffer, size ) =>
                {                 
                    remoteClient.SendPayload(buffer, size);                  
                };
                
                reliableEndpoint.SendMessage(payload, payloadSize, type);
            }
        }
    }
}