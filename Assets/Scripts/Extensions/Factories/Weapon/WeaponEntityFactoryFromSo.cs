using System;
using Extensions.Factories.Weapon.Bullets;
using Leopotam.Ecs;
using Model.Components.Extensions.EntityFactories;
using Model.Components.Requests;
using Model.Components.Weapons;
using Model.Timers;
using UnityEngine;

namespace Extensions.Factories.Weapon
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "SpaceDuel/Weapon", order = 10)]
    [Serializable]
    public class WeaponEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private ProjectileEntityFactoryFromSo _bulletEntityFactoryFromSo;
        [SerializeField] private BulletStartForce _bulletStartForce = new BulletStartForce {Value = 15f};
        [SerializeField] private DischargeShotContainer _dischargeShotContainer = new DischargeShotContainer {DischargeRequest = new DischargeRequest(){Value =.25f }};
        [SerializeField] private Muzzle _muzzle = new Muzzle {Offset = 2f};

        [SerializeField] private TimeBetweenShotsSetup _timeBetweenShotsSetup =
            new TimeBetweenShotsSetup {TimeSec = 0.15f};

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var gun = world.NewEntity();
            gun.Get<ShootIsPossible>();
            gun.Get<Muzzle>() = _muzzle;
            gun.Get<BulletStartForce>() = _bulletStartForce;
            gun.Get<TimeBetweenShotsSetup>() = _timeBetweenShotsSetup;
            gun.Get<DischargeShotContainer>() = _dischargeShotContainer;
            gun.Get<EntityFactoryRef<IEntityFactory>>().Value = _bulletEntityFactoryFromSo;
            return gun;
        }
    }
}