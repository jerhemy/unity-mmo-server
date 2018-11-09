using System.Text;

namespace GameServer
{
    public class ServerConfig
    {
        public int maxClients;		// int maximum number of clients which can connect to this server at one time
        public int publicAddress;
        public int port;	// string public address and int port clients will connect to
        public ulong protocolID;		// ulong protocol ID shared between clients and server
        public string privateKey; // byte[32] private crypto key shared between backend servers
    }
}