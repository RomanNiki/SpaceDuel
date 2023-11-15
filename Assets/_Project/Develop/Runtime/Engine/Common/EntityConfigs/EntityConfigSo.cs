using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Common.EntityConfigs
{
    public abstract class EntityConfigSo : ScriptableObject, IEntityConfig
    {
        public Entity Resolve(World world, Entity entity)
        {
            AddComponents(world, entity);
            return entity;
        }

        public Entity Resolve(World world)
        {
            var entity = world.CreateEntity();
            AddComponents(world, entity);
            return entity;
        }

        protected abstract void AddComponents(World world, Entity entity);
    }
}