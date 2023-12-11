using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Weapon.Components;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using _Project.Develop.Runtime.Engine.Services.Factories.EntitiesFactories.Weapons;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Damagers
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