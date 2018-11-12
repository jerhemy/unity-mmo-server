using System;
using System.Runtime.InteropServices;

namespace GameServer.Models
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)] 
    public struct ChatMessage
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=16)]
        public String from;
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=256)]
        public String message;
    }
}