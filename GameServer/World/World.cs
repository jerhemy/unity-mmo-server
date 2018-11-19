using System;
using System.Collections.Concurrent;
using System.Threading;
using NetcodeIO.NET;
using ReliableNetcode;

namespace GameServer.World
{
    public class WorldServer
    {
        private static bool _timeoutThread;
        private static object _timeoutThreadLock = new object();
        
        private SocketServer worldServer;
        private ConcurrentDictionary<RemoteClient, ClientAccount> worldClients;
        
        public WorldServer()
        {
            
        }

        
        public WorldServer(string Name, int port)
        {
            worldServer = new SocketServer("127.0.0.1");
            worldClients = new ConcurrentDictionary<RemoteClient, ClientAccount>();
            worldServer.OnDataReceived += DataRecieved;
            worldServer.OnClientConnected += ClientConnected;
            
            worldServer.StartServer();

            Thread t = new Thread(TimeoutClients);
            t.Start();
            
            Console.ReadLine();
            
            lock (_timeoutThreadLock)
            {
                _timeoutThread = false;
            }
            t.Join();
            
            worldServer.StopServer();
        }

        
        private void ClientConnected(RemoteClient client)
        {
            Console.WriteLine($"[{DateTime.Now}]: Server: Client Data Received: id:{client.ClientID}");
            worldClients.TryAdd(client, new ClientAccount {endpoint = new ReliableEndpoint()} );  
        }
        
        private void DataRecieved(RemoteClient client, byte[] payload, int payloadSize)
        {
            worldClients.TryGetValue(client, out var clientAccount);
            clientAccount.endpoint.ReceiveCallback = (buffer, size) =>
            {
                if (clientAccount.isAuthenticated)
                {
                    // Expect Packet to be Authentication
                }
                else
                {
                    // Read Login Data
                    var login = OP_Login.Deserialize(payload);
                    
                    // Validate Login
                    
                    // Assign Account Data

                    clientAccount.isAuthenticated = true;
                }
                
                
            };
        }

        private void TimeoutClients()
        {              
            foreach (var (remoteClient, clientAccount) in worldClients)
            {
                if (clientAccount.isAuthenticated) continue;
                
                if (clientAccount.createdAt.AddSeconds(10) < DateTime.Now)
                {
                    worldServer.DisconnectClient(remoteClient, "");
                }
            }
        }
    }
}