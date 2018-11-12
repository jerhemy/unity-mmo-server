using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using GameServer.Models;

namespace GameServer.DataAccess
{
    public class EntityRepository : SqLiteBaseRepository, IEntityRepository
    {
        private const string GET_ENTITY_BY_ID =
            @"SELECT Id, FirstName, LastName, DateOfBirth FROM Customer WHERE Id = @id";

        private const string INSERT_ENTITY =
            @"INSERT INTO entity ( name, zone, x, y, z, orientation, zone ) VALUES ( @name, @x, @y, @z, @orientation, @zone ); SELECT last_insert_rowid()";

        private const string CREATE_PLAYER_TABLE = @"create table player
            (id INTEGER PRIMARY KEY AUTOINCREMENT, name varchar(100) NOT NULL, x REAL NOT NULL, y REAL NOT NULL, z REAL NOT NULL, orientation REAL NOT NULL, zone INTEGER NOT NULL)";

        public Entity GetEntity(int id)
        {
            if (!File.Exists(DbFile)) CreateDatabase();

            using (var cnn = SQLiteDBConnection())
            {
                cnn.Open();
                Entity result = cnn.Query<Entity>(GET_ENTITY_BY_ID, new {id}).FirstOrDefault();
                return result;
            }
        }

        public List<Entity> GetEntitiesForZone(int zone)
        {
            throw new System.NotImplementedException();
        }

        public void SaveEntity(Entity entity)
        {
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }

            using (var cnn = SQLiteDBConnection())
            {
                cnn.Open();
                //SqlMapperExtensions.Update(cnn, entity);
            }
        }

        private static void CreateDatabase()
        {
            using (var cnn = SQLiteDBConnection())
            {
                cnn.Open();
                cnn.Execute(CREATE_PLAYER_TABLE);
            }
        }
    }
}