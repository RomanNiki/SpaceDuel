using Core.Characteristics.Enums;
using Core.Characteristics.Player.Components;
using Core.Extensions.Factories;
using Core.Input.Components;
using Core.Movement.Components;
using Core.Weapon.Components;
using Engine.Factories.EntitiesFactories.Weapons;
using Engine.Movement.Strategies;
using Engine.Views;
using Scellecs.Morpeh;
using UnityEngine;
using ViewObject = Core.Views.Components.ViewObject;

namespace Engine.Providers
{
    
    public sealed class SpaceShipEntityProvider : EntityProvider
    {
        [SerializeField] private TeamEnum _team;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private WeaponEntityFactorySo _primaryWeapon;
        [SerializeField] private WeaponEntityFactorySo _secondaryWeapon;
        
        protected override void OnInit()
        {
            SetData(new PlayerTag());
            SetData(new InputMoveData());
            AddMove();
            AddView();
            SetPrimaryWeapon();
            SetSecondaryWeapon();
        }

        private void AddView()
        {
            var moveStrategy = new RigidBodyMoveStrategy(_rigidbody);
            SetData(new ViewObject { Value = new UnityViewObject(this, moveStrategy) });
        }

        private void SetWeapon(IEntityFactory gunEntityFactoryFromSo, WeaponEnum weaponEnum)
        {
            var gun = gunEntityFactoryFromSo.CreateEntity(World);
            gun.SetComponent(new Owner(){Entity = Entity});
            gun.SetComponent(new WeaponType() { Value = weaponEnum });
        }

        private void SetPrimaryWeapon()
        {
            SetWeapon(_primaryWeapon, WeaponEnum.Primary);
        }

        private void SetSecondaryWeapon()
        {
            SetWeapon(_secondaryWeapon, WeaponEnum.Secondary);
        }
        
        private void AddMove()
        {
            SetData(new Position { Value = transform.position });
            SetData(new Rotation { Value = transform.eulerAngles.z });
            SetData(new Team(){Value = _team});
        }

        protected override void OnDispose()
        {
            Destroy(gameObject);
        }
    }
}