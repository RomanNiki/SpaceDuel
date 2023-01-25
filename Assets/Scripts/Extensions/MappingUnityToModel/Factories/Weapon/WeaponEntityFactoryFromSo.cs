using System;
using Extensions.MappingUnityToModel.Factories.Weapon.Bullets;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Extensions.EntityFactories;
using Model.Timers;
using Model.Timers.Components;
using Model.Unit.EnergySystems.Components.Requests;
using Model.Weapons.Components;
using UnityEngine;

namespace Extensions.MappingUnityToModel.Factories.Weapon
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "SpaceDuel/Weapon", order = 10)]
    [Serializable]
    public class WeaponEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private ProjectileEntityFactoryFromSo _bulletEntityFactoryFromSo;
        [SerializeField] private AudioClip _shot;
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
            gun.Get<UnityComponent<AudioClip>>().Value = _shot;
            return gun;
        }
    }
}