using System;
using Leopotam.Ecs;
using Model.Components.Tags;
using Model.Timers;
using Model.Timers.Components;
using Model.Unit.Movement.Components.Tags;
using Model.VisualEffects.Components.Tags;
using Model.Weapons.Components.Tags;
using UnityEngine;

namespace Extensions.MappingUnityToModel.Factories.Weapon.Bullets
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