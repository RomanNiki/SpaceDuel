using Leopotam.Ecs;
using Model.Components.Extensions.EntityFactories;
using UnityEngine;

namespace Extensions.Factories
{
    public abstract class EntityFactoryFromSo : ScriptableObject, IEntityFactory
    {
        public abstract EcsEntity CreateEntity(EcsWorld world);
    }
}