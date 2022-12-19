using System;
using Extensions.Factories.Weapon;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Extensions;
using Model.Components.Extensions.DyingPolicies;
using Model.Components.Extensions.EntityFactories;
using Model.Components.Tags;
using Model.Components.Tags.Effects;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents.Input;
using Model.Components.Weapons;
using Model.Enums;
using UnityEngine;

namespace Extensions.Factories
{
    [CreateAssetMenu(fileName = "Player", menuName = "SpaceDuel/Player", order = 10)]
    [Serializable]
    public class PlayerEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private Settings _settings;
        [SerializeField] private WeaponEntityFactoryFromSo _primaryWeapon;
        [SerializeField] private WeaponEntityFactoryFromSo _secondaryWeapon;

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<PlayerTag>();
            entity.Get<InputMoveData>();
            entity.Get<ExplosiveTag>();
            entity.AddHealth(_settings.MaxHealth, new StandardDyingPolicy())
                .AddEnergy(_settings.MaxEnergy);
            entity.Get<DischargeMoveContainer>().DischargeRequest.Value = _settings.MoveCost;
            entity.Get<DischargeRotateContainer>().DischargeRequest.Value = _settings.RotationCost;
            SetPrimaryWeapon(entity, _primaryWeapon, world);
            SetSecondaryWeapon(entity, _secondaryWeapon, world);
            return entity;
        }

        private static EcsEntity SetWeapon(in EcsEntity player,
            IEntityFactory gunEntityFactoryFromSo, WeaponEnum weaponEnum, EcsWorld world)
        {
            var gun = gunEntityFactoryFromSo.CreateEntity(world);
            gun.Get<PlayerOwner>().Owner = player;
            gun.Get<WeaponType>() = new WeaponType() {Type = weaponEnum};
            gun.Get<ShootIsPossible>();
            return gun;
        }

        private static void SetPrimaryWeapon(in EcsEntity player,
            in IEntityFactory gunEntityFactoryFromSo, EcsWorld world)
        {
            SetWeapon(player, gunEntityFactoryFromSo, WeaponEnum.Primary, world);
        }

        private static void SetSecondaryWeapon(in EcsEntity player,
            in IEntityFactory gunEntityFactoryFromSo, EcsWorld world)
        {
            SetWeapon(player, gunEntityFactoryFromSo, WeaponEnum.Secondary, world);
        }

        [Serializable]
        public class Settings
        {
            public float MaxHealth;
            public float MaxEnergy;
            public float RotationCost;
            public float MoveCost;
        }
    }
}