using System.Data;
using MySql.Data.MySqlClient;

namespace GameServer.DataAccess.Repository
{
    public class BaseRespository
    {
        public IDbConnection Connection
        {
            get
            {
                return new MySqlConnection("Server=192.168.0.50;Database=UnityMMO;Uid=developer;Pwd=password;");
            }
        }
    }
}