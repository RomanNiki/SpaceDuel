using Core.Common.Enums;
using Core.Extensions.Factories;
using Core.Weapon.Components;
using Engine.Factories.EntitiesFactories.Weapons;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers
{
    public sealed class SpaceShipEntityProvider : EntityProvider
    {
        [SerializeField] private WeaponEntityFactorySo _primaryWeapon;
        [SerializeField] private WeaponEntityFactorySo _secondaryWeapon;

        protected override void OnInit()
        {
            SetPrimaryWeapon();
            SetSecondaryWeapon();
        }

        private void SetWeapon(IEntityFactory gunEntityFactoryFromSo, WeaponEnum weaponEnum)
        {
            var gun = gunEntityFactoryFromSo.CreateEntity(World);
            gun.SetComponent(new Owner() { Entity = Entity });
            gun.SetComponent(new WeaponType() { Value = weaponEnum });
        }

        private void SetPrimaryWeapon() => SetWeapon(_primaryWeapon, WeaponEnum.Primary);


        private void SetSecondaryWeapon() => SetWeapon(_secondaryWeapon, WeaponEnum.Secondary);

        protected override void OnDispose() => Destroy(gameObject);
    }
}