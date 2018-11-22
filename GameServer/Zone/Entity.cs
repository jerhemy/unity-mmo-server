using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using NetcodeIO.NET;

namespace GameServer.Zone
{   
    public class Entity
    {
        public Entity()
        {
            
        }
        
        public int Id;

        public string Name;
        
        public Vector3 Position;

        public ConcurrentDictionary<Int64, Entity> proximity_list;

        public virtual void Process(ref EntityList entity_list)
        {
            proximity_list.Clear();
            // Find Proximity Entities
            foreach (var (key, entity) in entity_list._clientList)
            {
                if (Intersects(entity, 2.0f))
                {
                    proximity_list.TryAdd(key, (Entity)entity);
                }
            }
            
            foreach (var (key, entity) in entity_list._mobList)
            {
                if (Intersects(entity, 2.0f))
                {
                    proximity_list.TryAdd(key, (Entity)entity);
                }
            }
        }
        
        public bool Intersects(Entity entity, float radius)
        {
            return (Math.Pow(entity.Position.X - Position.X, 2) + Math.Pow(entity.Position.Y - Position.Y, 2)) < (Math.Pow(radius, 2));
        }
        
        
    }
}