using Core.Extensions;
using Core.Extensions.Factories;
using Core.Timers.Components;
using Core.Weapon.Components;
using Engine.Factories.EntitiesFactories.Projectiles;
using Engine.Views.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Factories.EntitiesFactories.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "SpaceDuel/Weapon", order = 10)]
    public class WeaponEntityFactorySo : EntityFactoryFromSo
    {
        [SerializeField] private ProjectileEntityFactorySo _bulletEntityFactoryFromSo;
        [SerializeField] private AudioClip _shot;
        [SerializeField] private BulletStartForce _bulletStartForce = new() { Value = 15f };

        [SerializeField] private DischargeContainer _dischargeContainer =
            new() { Value = .25f };

        [SerializeField] private Muzzle _muzzle = new() { Offset = 2f };

        [SerializeField] private TimerBetweenShotsSetup _timeBetweenShotsSetup =
            new() { TimeSec = 0.15f };

        public override Entity CreateEntity(in World world)
        {
            var entity = world.CreateEntity();
            world.AddComponentToEntity(entity, _muzzle);
            world.AddComponentToEntity(entity, _bulletStartForce);
            world.AddComponentToEntity(entity, _timeBetweenShotsSetup);
            world.AddComponentToEntity(entity, _dischargeContainer);
            world.AddComponentToEntity(entity, new EntityFactoryRef<IEntityFactory>(_bulletEntityFactoryFromSo));
            world.AddComponentToEntity(entity, new UnityComponent<AudioClip>(_shot));

            return entity;
        }
    }
}