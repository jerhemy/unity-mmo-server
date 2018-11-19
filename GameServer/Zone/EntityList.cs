using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using GameServer.DataAccess.Models;
using NetcodeIO.NET;

namespace GameServer.Zone
{
    public class EntityList
    {
        public EntityList()
        {
            
        }

        private ConcurrentDictionary<ulong, Character> client_list = new ConcurrentDictionary<ulong, Character>();
        private ConcurrentDictionary<RemoteClient, ulong> reverse_client_list = new ConcurrentDictionary<RemoteClient, ulong>();
        
        public ConcurrentDictionary<ulong, Mob> mob_list = new ConcurrentDictionary<ulong, Mob>();

        public Character GetClientByRemoteClient(RemoteClient remoteClient)
        {
            if (reverse_client_list.TryGetValue(remoteClient, out var clientId))
            {
                if (client_list.TryGetValue(clientId, out var character))
                    return character;
            }

            return null;
        }

        public bool AddClient(RemoteClient remoteClient, Character client)
        {
            var radd = reverse_client_list.TryAdd(remoteClient, client.Id);
            var add = client_list.TryAdd(client.Id, client);
            return radd && add;
        }
        
        public void Process()
        {
            
        }
    }
}