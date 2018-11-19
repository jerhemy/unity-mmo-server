using System;
using System.Numerics;
using GameServer.DataAccess.Models;

namespace GameServer.Zone
{
    public class Mob : Entity
    {        
        public Mob()
        {
            
        }
        
        protected override bool IsClient() { return false; }
        
        public string name { get; set; }




    }
}