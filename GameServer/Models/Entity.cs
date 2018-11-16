using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace GameServer.Models
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)] 
    public struct Entity
    {
        public int id;
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=16)]
        public String name;

        public SimpleVector3 loc;
        public float  orientation;

        public float vX;
        public float vY;
        public float vZ;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)]
        public SimpleVector3[] waypoints;
    }


    public class CEntity
    {
        private int Id { get; set; }

        private string name;
        
        private Vector3 position;
        private Quaternion orientation;
        private Vector3[] Waypoints;

        public CEntity(int Id)
        {
            this.Id = Id;
        }
        
        
        
    }
}