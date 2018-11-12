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
        
        /* Positioning */
        public double x;
        public double y;
        public double z;
        public float  orientation;
    }
}