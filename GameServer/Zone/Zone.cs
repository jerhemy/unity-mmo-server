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
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;
             
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
                        
            //Load Zone Mobs
           
            _socket.OnClientConnected += OnClientConnected;
            _socket.OnClientConnected += OnClientDisconnected;
            _socket.OnDataReceived += OnDataReceived;
        }

        public bool Start()
        {
            try
            {
                _socket.StartServer();             
                Task.Run(() => { entity_list.Process(); }, token);                  
                return true;
            }
            catch (Exception e)
            {
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

        private bool LoadZoneObjects()
        {
            //Load Zone Doors
            
            //Load Zone Traps
            
            //Load Zone Artifcats   

            return true;
        }

        private bool LoadGroundSpawns()
        {
            return true;
        }
        
        private bool LoadMobSpawns()
        {
            return true;
        }

        private void Process()
        {
            while (true)
            {
                
            }
        }
        private void OnClientConnected(RemoteClient client)
        {
            // Load Character
            
            
        }

        private void OnClientDisconnected(RemoteClient client)
        {
                    
        }
        
        private void OnDataReceived(RemoteClient remoteClient, byte[] payload, int payloadSize)
        {            
            entity_list.GetClientByRemoteClient(remoteClient).ProcessMessage(payload, payloadSize);
        }
    }
}