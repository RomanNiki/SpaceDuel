using System;
using Leopotam.Ecs;
using Model.Unit.EnergySystems.Components;
using Model.Unit.Movement.Components.Tags;
using Model.Unit.SunEntity.Components;
using Model.VisualEffects.Components.Tags;
using Model.Weapons.Components.Tags;
using UnityEngine;
using UnityEngine.Serialization;

namespace Extensions.MappingUnityToModel.Factories.Weapon.Bullets
{
    [CreateAssetMenu(fileName = "Mine", menuName = "SpaceDuel/Projectiles/Mine", order = 10)]
    [Serializable]
    public sealed class MineEntityFactoryFromSo : ProjectileEntityFactoryFromSo
    {
        [FormerlySerializedAs("_sunGravityResistTime")] [SerializeField] private float _startEnergy = 40;

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = base.CreateEntity(world);
            entity.Get<MineTag>();
            entity.Get<GravityResist>();
            entity.Get<ExplosiveTag>();
            entity.Get<SunDischargeTag>();
            entity.Get<Energy>().Current = _startEnergy;
            return entity;
        }
    }
}