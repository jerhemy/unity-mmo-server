using NetcodeIO.NET;
using ReliableNetcode;

namespace GameServer.Zone
{
    public partial class Character : Mob
    {
        public EntityList list;

        public Character(EntityList list)
        {
            this.list = list;
        }
        
        protected override bool IsClient() { return true; }
        
        private RemoteClient _client;
        private ReliableEndpoint _endpoint;
    }
}