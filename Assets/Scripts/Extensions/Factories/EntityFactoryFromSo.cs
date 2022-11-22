using Components.Extensions.EntityFactories;
using Leopotam.Ecs;
using UnityEngine;

namespace Extensions.Factories
{
    public abstract class EntityFactoryFromSo : ScriptableObject, IEntityFactory
    {
        public abstract EcsEntity CreateEntity(EcsWorld world);
    }
}