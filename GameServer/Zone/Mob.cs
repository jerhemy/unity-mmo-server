using System;
using System.Numerics;
using GameServer.DataAccess.Models;

namespace GameServer.Zone
{
    public class Mob : Entity
    {        
        public Mob()
        {
            Name = "";

            Position = new Vector3(0,0,0);
            
            STR = 0;
            CON = 0;
            AGI = 0;
            DEX = 0;
            INT = 0;
            WIS = 0;
            CHR = 0;

            IsDead = false;

            SelectedTarget = null;
        }


        public bool IsDead;
        
        public string Name { get; set; }

        
        public Vector3 Position;
        
        public int? SelectedTarget { get; set; }



        // Stats
        public int STR;
        public int CON;
        public int AGI;
        public int DEX;
        public int INT;
        public int WIS;
        public int CHR;

        private GameTimer AbilityTimer1;
        private GameTimer AbilityTimer2;
        private GameTimer AbilityTimer3;
        private GameTimer AbilityTimer4;

        public void Process()
        {
            
        }

        public void GetProximityEntities()
        {
            
        }
    }
}