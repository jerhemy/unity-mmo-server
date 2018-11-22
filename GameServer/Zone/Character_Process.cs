using System;
using System.IO;
using System.Numerics;
using ReliableNetcode;

namespace GameServer.Zone
{
    public partial class Character
    {
        private ReliableEndpoint _endpoint;
        
        public void ProcessMessage(byte[] payload, int payloadSize)
        {
            using(var ms = new MemoryStream(payload))
            using (var br = new BinaryReader(ms))
            {
                var type = br.ReadInt16();
                switch (type)
                {
                    case (short)ClientMessageType.OP_PositionUpdate:
                        var X = br.ReadSingle();
                        var Y = br.ReadSingle();
                        var Z = br.ReadSingle();
                        this.Position = new Vector3(X, Y, Z);
                        Console.WriteLine(this.Position.ToString());
                        break;
                    case (short)ClientMessageType.OP_ChatMessage:
                        
                        break;                                           
                }
                
            }
        }
    }
}