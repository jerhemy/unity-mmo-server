using System.IO;

namespace GameServer.World
{
    public struct OP_Login
    {
        public string Username;
        public string Password;

        public static OP_Login Deserialize(byte[] payload)
        {
            using (var ms = new MemoryStream(payload))
            {
                using (var br = new BinaryReader(ms))
                {
                    var username = br.ReadString();
                    var password = br.ReadString();

                    return new OP_Login {Username = username, Password = password};
                }
            }
        }
    }

    public struct OP_CharacterCreate
    {
        public int Race;
        public short Gender;
        public int Class;
        public int Diety;
        
        public int STR;
        public int CON;
        public int AGI;
        public int DEX;
        public int INT;
        public int WIS;
        public int CHA;

        public string Name;
    }
}