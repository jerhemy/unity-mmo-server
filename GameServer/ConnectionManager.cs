using System;
using System.Collections.Concurrent;
using System.Text;
using NetcodeIO.NET;
using ReliableNetcode;

namespace GameServer
{
    public class ConnectionManager
    {
        static readonly byte[] _privateKey = new byte[]
        {
            0x60, 0x6a, 0xbe, 0x6e, 0xc9, 0x19, 0x10, 0xea,
            0x9a, 0x65, 0x62, 0xf6, 0x6f, 0x2b, 0x30, 0xe4,
            0x43, 0x71, 0xd6, 0x2c, 0xd1, 0x99, 0x27, 0x26,
            0x6b, 0x3c, 0x60, 0xf4, 0xb7, 0x15, 0xab, 0xa1,
        };
        
        private static Server _server;

        private ConcurrentDictionary<RemoteClient, ReliableEndpoint> _clients;
        
        public ConnectionManager()
        {
            _clients = new ConcurrentDictionary<RemoteClient, ReliableEndpoint>();    
        }
        
        public void StartServer()
        {
            //var privateKeyBytes = Encoding.ASCII.GetBytes("thisismysupersecretkeyimusing");
            
            _server = new Server(
                5,		// int maximum number of clients which can connect to this server at one time
                "127.0.0.1", 8559,	// string public address and int port clients will connect to
                1UL,		// ulong protocol ID shared between clients and server
                _privateKey		// byte[32] private crypto key shared between backend servers
            );
            
            // Called when a client has connected
            _server.OnClientConnected += clientConnectedHandler;		// void( RemoteClient client )

            // Called when a client disconnects
            _server.OnClientDisconnected += clientDisconnectedHandler;	// void( RemoteClient client )

            // Called when a payload has been received from a client
            // Note that you should not keep a reference to the payload, as it will be returned to a pool after this call completes.
            _server.OnClientMessageReceived += messageReceivedHandler;	// void( RemoteClient client, byte[] payload, int payloadSize )
            
            _server.Start(); 
            Console.WriteLine("Waiting for connections...");
        }

        public void StopServer(int secondsToStop = 0)
        {
            if (secondsToStop == 0)
            {
                foreach (var (remoteClient, reliableEndpoint) in _clients)
                {
                    _server.Disconnect(remoteClient);
                }

                _server.Stop();
            }
        }

        public void SendAll(byte[] payload, int payloadSize)
        {

        }
        public void SendTo(RemoteClient client, byte[] payload, int payloadSize)
        {
            
        }
        
        private void clientConnectedHandler(RemoteClient client)
        {           
            Console.WriteLine($"clientConnectedHandler: {client}");
            _clients.TryAdd(client, new ReliableEndpoint());
        }
        
        private void clientDisconnectedHandler(RemoteClient client)
        {
            Console.WriteLine($"clientDisconnectedHandler: {client}");
            _clients.TryRemove(client, out _);
        }
        
        private static void messageReceivedHandler(RemoteClient client, byte[] payload, int payloadSize)
        {
            var pos = StructTools.RawDeserialize<Position>(payload, 0); // 0 is offset in byte[]
            //Console.WriteLine($"messageReceivedHandler: {client} sent {payloadSize} bytes of data.");
            Console.WriteLine($"X:{pos.X} Y:{pos.Y} Z:{pos.Z}");
        }
        
    }
}