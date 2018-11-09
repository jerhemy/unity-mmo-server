using System.Runtime.InteropServices;

namespace GameServer
{
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 1)]
    public struct Position
    {
        [FieldOffset(0)]
        public float X;
        [FieldOffset(4)]
        public float Y;
        [FieldOffset(8)]
        public float Z;
    }
}