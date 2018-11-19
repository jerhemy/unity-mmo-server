using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GameServer.DataAccess.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace GameServer.DataAccess.Repository
{
    public interface IMobRepository
    {
        Task<DBMob> GetByID(int id);
        Task<List<DBMob>> GetByZoneName(string zoneName);
    }
    
    public class MobRepository : BaseRespository
    {

        public MobRepository()
        {

        }
        
        public async Task<DBMob> GetByID(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT id, name, x, y, z FROM mobs WHERE ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<DBMob>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<DBMob>> GetByZoneName(string zoneName)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT id, name, x, y, z FROM mobs WHERE zone = @ZoneName";
                conn.Open();
                var result = await conn.QueryAsync<DBMob>(sQuery, new { ZoneName = zoneName });
                return result.ToList();
            }
        }
    }
}