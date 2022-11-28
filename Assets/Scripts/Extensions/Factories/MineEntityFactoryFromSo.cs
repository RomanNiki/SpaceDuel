using System;
using Leopotam.Ecs;
using Model.Components.Tags;
using Model.Timers;
using UnityEngine;

namespace Extensions.Factories
{
    [CreateAssetMenu(fileName = "Mine", menuName = "SpaceDuel/Mine", order = 10)]
    [Serializable]
    public class MineEntityFactoryFromSo : ProjectileEntityFactoryFromSo
    {
        [SerializeField] private float _sunGravityResistTime = 1;

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = base.CreateEntity(world);
            entity.Get<MineTag>();
            entity.Get<NoGravity>();
            entity.Get<Timer<SunGravityResistTime>>().TimeLeftSec = _sunGravityResistTime;
            return entity;
        }
    }
}