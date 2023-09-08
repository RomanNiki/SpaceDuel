using Core.Characteristics.EnergyLimits.Components;
using Core.Extensions;
using Core.Extensions.Factories;
using Core.Movement.Components;
using Core.Views.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Weapon.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public class ExecuteShootSystem : ISystem
    {
        private Filter _shotFilter;
        private Stash<Owner> _ownerPool;
        private Stash<NoEnergyBlock> _energyBlockPool;
        private Stash<ShootingRequest> _shootingPool;
        private Stash<Position> _positionPool;
        private Stash<Rotation> _rotationPool;
        private Stash<Velocity> _velocityPool;
        private Stash<BulletStartForce> _startForcePool;
        private Stash<EntityFactoryRef<IEntityFactory>> _entityFactoryPool;
        private Stash<Muzzle> _muzzlePool;
        public World World { get; set; }

        public void OnAwake()
        {
            _shotFilter = World.Filter.With<ShootingRequest>().Build();
            _ownerPool = World.GetStash<Owner>();
            _energyBlockPool = World.GetStash<NoEnergyBlock>();
            _shootingPool = World.GetStash<ShootingRequest>();
            _positionPool = World.GetStash<Position>();
            _velocityPool = World.GetStash<Velocity>();
            _rotationPool = World.GetStash<Rotation>();
            _startForcePool = World.GetStash<BulletStartForce>();
            _entityFactoryPool = World.GetStash<EntityFactoryRef<IEntityFactory>>();
            _muzzlePool = World.GetStash<Muzzle>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var shootingRequestEntity in _shotFilter)
            {
                var shootingRequest = _shootingPool.Get(shootingRequestEntity);
                var weaponEntity = shootingRequest.Entity;
                var direction = shootingRequest.Direction;
                World.RemoveEntity(shootingRequestEntity);
                if (weaponEntity.IsNullOrDisposed())
                    continue;
                
                var ownerEntity = _ownerPool.Get(weaponEntity).Entity;
                if (ownerEntity.IsNullOrDisposed())
                    continue;

                if (_energyBlockPool.Has(ownerEntity))
                    continue;
                

                ref var factory = ref _entityFactoryPool.Get(weaponEntity);
                ref var bulletForce = ref _startForcePool.Get(weaponEntity).Value;
                var spawnPosition = _positionPool.Get(ownerEntity).Value +
                                    (Vector2)_rotationPool.Get(ownerEntity).LookDir *
                                    _muzzlePool.Get(weaponEntity).Offset;

                CreateBullet(factory.Factory, spawnPosition, direction * bulletForce);
                MessageShotMade(weaponEntity);
            }
        }

        private void MessageShotMade(Entity weaponEntity)
        {
            World.SendMessage(new ShotMadeEvent() { Weapon = weaponEntity });
        }

        private void CreateBullet(IEntityFactory factory, Vector2 spawnPosition, Vector2 velocity)
        {
            var entity = factory.CreateEntity(World);
            if (entity.IsNullOrDisposed()) return;

            _velocityPool.Get(entity).Value = velocity;
            _positionPool.Get(entity).Value = spawnPosition;
            var rotation = MathExtensions.CalculateRotationFromVelocity(velocity);
            _rotationPool.Get(entity).Value = rotation;
            World.SendMessage(new ViewCreateRequest(entity, spawnPosition, rotation));
        }

        public void Dispose()
        {
        }
    }
}