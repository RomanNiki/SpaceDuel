using Leopotam.Ecs;
using Model.Components.Tags.Buffs;
using Model.Timers;
using UnityEngine;

namespace Extensions.MappingUnityToModel.Factories.Buffs
{
    public class BuffEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private float _lifeTime = 10f;
        
        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<BuffTag>();
            entity.Get<Timer<LifeTime>>().TimeLeftSec = _lifeTime;
            return entity;
        }
    }
}