using Leopotam.Ecs;
using Model.Extensions.EntityFactories;
using UnityEngine;

namespace Extensions.MappingUnityToModel.Factories
{
    public abstract class EntityFactoryFromSo : ScriptableObject, IEntityFactory
    {
        public abstract EcsEntity CreateEntity(EcsWorld world);
    }
}