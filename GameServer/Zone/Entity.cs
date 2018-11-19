using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using NetcodeIO.NET;

namespace GameServer.Zone
{   
    public class Entity
    {
        public RemoteClient client;
        
        protected virtual bool IsClient() { return false; }
        
        public ulong Id { get; set; }

        private string name;
        
        private Vector3 position;
        private Quaternion orientation;
        private Vector3[] Waypoints;

        public int selectedTarget { get; set; }
    }
}