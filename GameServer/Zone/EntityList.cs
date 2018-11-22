using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using GameServer.DataAccess.Models;
using NetcodeIO.NET;

namespace GameServer.Zone
{
    public class EntityList
    {
        private Int64 _entityId;
        
        public EntityList()
        {
            _entityId = 0;
        }

        public Int64 GetID()
        {
            Interlocked.Increment(ref _entityId);
            return _entityId;
        }
        
        public readonly ConcurrentDictionary<Int64, Character> _clientList = new ConcurrentDictionary<Int64, Character>();
        private readonly ConcurrentDictionary<RemoteClient, Int64> _reverseClientList = new ConcurrentDictionary<RemoteClient, Int64>();
        public readonly ConcurrentDictionary<Int64, Mob> _mobList = new ConcurrentDictionary<Int64, Mob>();

        public bool AddClient(RemoteClient remoteClient, Character client)
        {
            var id = GetID();
            return _reverseClientList.TryAdd(remoteClient, id) && _clientList.TryAdd(id, client);
        }

        public bool RemoveClient(RemoteClient remoteClient)
        {
            return _reverseClientList.TryRemove(remoteClient, out var id) && _clientList.TryRemove(id, out var character);
        }
        
        public bool AddMob(Mob mob)
        {
            return _mobList.TryAdd(GetID(), mob);
        }   
        
        public void ProcessMessage(RemoteClient remoteClient, byte[] payload, int payloadSize)      
        {
            // Find Client who sent the message and then process
            GetCharacterByRemoteClient(remoteClient).ProcessMessage(payload, payloadSize);
        }
        
        private Character GetCharacterByRemoteClient(RemoteClient remoteClient)
        {
            if (_reverseClientList.TryGetValue(remoteClient, out var clientId))
            {
                if (_clientList.TryGetValue(clientId, out var character))
                    return character;
            }

            return null;
        }

        public void Process()
        {
            
        }
    }
}