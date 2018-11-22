using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using GameServer.DataAccess.Repository;
using GameServer.Models;
using NetcodeIO.NET;

namespace GameServer.Zone
{
    public class Zone
    {
        static CancellationTokenSource tokenSource = new CancellationTokenSource();
        static CancellationToken  token = tokenSource.Token;
             
        private bool IsZoneLoaded { get; set; }
        private Vector3 _SafePoint;
        private Vector3 _GraveYard;
        
        private EntityList entity_list;
        
        private static SocketServer _socket;

        private string zoneName;

        private MobRepository _mobRepository;
        
        public Zone(string zoneName)
        {
            this.zoneName = zoneName;
            this.entity_list = new EntityList();
            _socket = new SocketServer("127.0.0.1");
            _mobRepository = new MobRepository();
            
            //Load Zone Mobs
            //LoadMobSpawns();

            _socket.OnClientConnected += OnClientConnected;
            _socket.OnClientConnected += OnClientDisconnected;
            _socket.OnDataReceived += OnDataReceived;
            //_socket.OnDataReceived += entity_list.ProcessMessage;
        }

        public bool Start()
        {
            try
            {
                _socket.StartServer();             
                Task.Run(() =>
                {
                    while (true)
                    {
                        entity_list.Process();
                    }

                }, token);
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Stop()
        {
            try
            {
                tokenSource.Cancel();
                _socket.StopServer();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async void LoadZoneObjects()
        {
            var mobs = await _mobRepository.GetByZoneName(zoneName);
            mobs.ForEach(m =>
            {
                Mob mob = new Mob {Name = m.Name, Position = new Vector3(m.x, m.y, m.z)};
                entity_list.AddMob(mob);
            });
            
            Console.WriteLine($"{mobs.Count} Mobs Loaded...");
        }

        private async void LoadGroundSpawns()
        {
            var mobs = await _mobRepository.GetByZoneName(zoneName);
            mobs.ForEach(m =>
            {
                Mob mob = new Mob {Name = m.Name, Position = new Vector3(m.x, m.y, m.z)};
                entity_list.AddMob(mob);
            });
            
            Console.WriteLine($"{mobs.Count} Mobs Loaded...");
        }
        
        private async void LoadMobSpawns()
        {
            var mobs = await _mobRepository.GetByZoneName(zoneName);
            mobs.ForEach(m =>
            {
                Mob mob = new Mob {Name = m.Name, Position = new Vector3(m.x, m.y, m.z)};
                entity_list.AddMob(mob);
            });
            
            Console.WriteLine($"{mobs.Count} Mobs Loaded...");
        }

        private void Process()
        {

        }
        
        private void OnClientConnected(RemoteClient client)
        {
            Console.WriteLine($"ClientID: {client.ClientID}");
            // Add Client to List with Empty Character       
            entity_list.AddClient(client, new Character());            
        }

        private void OnClientDisconnected(RemoteClient client)
        {   
        }
        
        private void OnDataReceived(RemoteClient remoteClient, byte[] payload, int payloadSize)
        {
            entity_list.ProcessMessage(remoteClient, payload, payloadSize);
        }
    }
}