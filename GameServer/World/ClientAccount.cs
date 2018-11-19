using System;
using ReliableNetcode;

namespace GameServer.World
{
    public class ClientAccount
    {
        public ReliableEndpoint endpoint { get; set; }
        public DateTime createdAt;

        private bool _isAuthenticated = false;
        
        public ClientAccount()
        {
            createdAt = new DateTime();
        }

        public bool isAuthenticated
        {
            get => _isAuthenticated;
            set => _isAuthenticated = value;
        }

        private UInt32 AccountId;
        
        
    }
}