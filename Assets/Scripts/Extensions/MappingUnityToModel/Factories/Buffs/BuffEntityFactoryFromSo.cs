using Leopotam.Ecs;
using Model.Buffs.Components.Tags;
using Model.Timers;
using Model.Timers.Components;
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