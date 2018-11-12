using System;
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
}