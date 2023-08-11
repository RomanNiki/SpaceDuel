using Core.Extensions.Factories;
using Core.Movement.Components;
using Core.Player.Components;
using Core.Views.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Weapon.Systems
{
    public class ExecuteShootSystem : ISystem
    {
        private Filter _shotFilter;
        private Stash<PlayerOwner> _ownerPool;
        private Stash<NoEnergyBlock> _energyBlockPool;
        private Stash<Shooting> _shootingPool;
        private Stash<Position> _positionPool;
        private Stash<Rotation> _rotationPool;
        private Stash<Velocity> _velocityPool;
        private Stash<BulletStartForce> _startForcePool;
        private Stash<EntityFactoryRef<IEntityFactory>> _entityFactoryPool;
        private Stash<Muzzle> _muzzlePool;
        private Stash<ViewCreateRequest> _viewCreatePool;
        private Stash<ShotMadeEvent> _shotEventPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _shotFilter = World.Filter.With<PlayerOwner>().With<Shooting>().With<EntityFactoryRef<IEntityFactory>>()
                .With<BulletStartForce>().With<Muzzle>();
            _ownerPool = World.GetStash<PlayerOwner>();
            _energyBlockPool = World.GetStash<NoEnergyBlock>();
            _shootingPool = World.GetStash<Shooting>();
            _positionPool = World.GetStash<Position>();
            _rotationPool = World.GetStash<Rotation>();
            _startForcePool = World.GetStash<BulletStartForce>();
            _entityFactoryPool = World.GetStash<EntityFactoryRef<IEntityFactory>>();
            _muzzlePool = World.GetStash<Muzzle>();
            _viewCreatePool = World.GetStash<ViewCreateRequest>();
            _shotEventPool = World.GetStash<ShotMadeEvent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var weaponEntity in _shotFilter)
            {
                var ownerEntity = _ownerPool.Get(weaponEntity).Entity;
                if (ownerEntity == null)
                    continue;

                if (_energyBlockPool.Has(ownerEntity))
                    continue;
                ref var factory = ref _entityFactoryPool.Get(weaponEntity);
                ref var direction = ref _shootingPool.Get(weaponEntity).Direction;
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
            _shotEventPool.Add(weaponEntity);
        }

        private void CreateBullet(IEntityFactory factory, Vector2 spawnPosition, Vector2 velocity)
        {
            var entity = factory.CreateEntity(World);
            if (entity != null)
            {
                _velocityPool.Get(entity).Value = velocity;
                _positionPool.Get(entity).Value = spawnPosition;
                _rotationPool.Get(entity).Value = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                _viewCreatePool.Add(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}