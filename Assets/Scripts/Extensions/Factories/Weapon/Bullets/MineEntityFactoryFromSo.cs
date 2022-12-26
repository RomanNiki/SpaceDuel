using System;
using Leopotam.Ecs;
using Model.Components.Tags;
using Model.Components.Tags.Effects;
using Model.Components.Tags.Projectiles;
using Model.Timers;
using UnityEngine;

namespace Extensions.Factories.Weapon.Bullets
{
    [CreateAssetMenu(fileName = "Mine", menuName = "SpaceDuel/Projectiles/Mine", order = 10)]
    [Serializable]
    public sealed class MineEntityFactoryFromSo : ProjectileEntityFactoryFromSo
    {
        [SerializeField] private float _sunGravityResistTime = 1;

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = base.CreateEntity(world);
            entity.Get<MineTag>();
            entity.Get<GravityResist>();
            entity.Get<ExplosiveTag>();
            entity.Get<Timer<SunGravityResistTime>>().TimeLeftSec = _sunGravityResistTime;
            return entity;
        }
    }
}