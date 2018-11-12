using System.Runtime.InteropServices;

namespace GameServer
{
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 1)]
    public struct Position
    {
        [FieldOffset(0)] public float x;
        [FieldOffset(4)] public float y;
        [FieldOffset(8)] public float z;
		
        [FieldOffset(12)] public float rotX;
        [FieldOffset(16)] public float rotY;
        [FieldOffset(20)] public float rotZ;
        
        public override string ToString()
        {
            return $"X:{x} Y:{y} Z:{z}";
        }
    }
}