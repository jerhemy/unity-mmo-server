using System.Collections.Generic;

namespace GameServer.Models
{
    public interface IEntityRepository
    {
        Entity GetEntity(int id);
        List<Entity> GetEntitiesForZone(int zone);
        void SaveEntity(Entity entity);
    }
}