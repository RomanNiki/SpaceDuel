using Core.Common.Enums;
using Core.Weapon.Components;
using Engine.Providers.MonoProviders.Base;
using Engine.Services.Factories.EntitiesFactories.Weapons;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers.MonoProviders.Damagers
{
    public class WeaponProvider : MonoProviderBase
    {
        [SerializeField] private WeaponEntityFactorySo _weaponFactory;
        [SerializeField] private WeaponEnum _weaponType;
        
        
        public override void Resolve(World world, Entity entity)
        {
            SetWeapon(world, entity);
        }
        
        private void SetWeapon(World world, Entity entity)
        {
            var gun = _weaponFactory.CreateEntity(world);
            gun.SetComponent(new Owner { Entity = entity });
            gun.SetComponent(new WeaponType { Value = _weaponType });
        }
    }
}