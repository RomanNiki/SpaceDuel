using Core.Characteristics.EnergyLimits.Components;
using Core.Common.Enums;
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
        private Stash<BulletStartForce> _startForcePool;
        private Stash<ShootObjectType> _shootTypePool;
        private Stash<EntityFactoryRef<IEntityFactory>> _entityFactory;
        private Stash<Muzzle> _muzzlePool;
        public World World { get; set; }

        public void OnAwake()
        {
            _shotFilter = World.Filter.With<ShootingRequest>().Build();
            _ownerPool = World.GetStash<Owner>();
            _energyBlockPool = World.GetStash<NoEnergyBlock>();
            _shootingPool = World.GetStash<ShootingRequest>();
            _positionPool = World.GetStash<Position>();
            _rotationPool = World.GetStash<Rotation>();
            _startForcePool = World.GetStash<BulletStartForce>();
            _shootTypePool = World.GetStash<ShootObjectType>();
            _entityFactory = World.GetStash<EntityFactoryRef<IEntityFactory>>();
            _muzzlePool = World.GetStash<Muzzle>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var shootingRequestEntity in _shotFilter)
            {
                var shootingRequest = _shootingPool.Get(shootingRequestEntity);
                var weaponEntity = shootingRequest.Entity;
                var direction = shootingRequest.Direction;
                if (weaponEntity.IsNullOrDisposed())
                    continue;

                var ownerEntity = _ownerPool.Get(weaponEntity).Entity;
                if (ownerEntity.IsNullOrDisposed())
                    continue;

                if (_energyBlockPool.Has(ownerEntity))
                    continue;


                ref var shootType = ref _shootTypePool.Get(weaponEntity);
                ref var bulletForce = ref _startForcePool.Get(weaponEntity).Value;
                var spawnPosition = _positionPool.Get(ownerEntity).Value +
                                    (Vector2)_rotationPool.Get(ownerEntity).LookDir *
                                    _muzzlePool.Get(weaponEntity).Offset;
                ref var entityFactory = ref _entityFactory.Get(weaponEntity);
                CreateBullet(entityFactory.Factory, shootType.ObjectId, spawnPosition, direction * bulletForce);
                MessageShotMade(weaponEntity);
            }
        }

        private void MessageShotMade(Entity weaponEntity) =>
            World.SendMessage(new ShotMadeEvent { Weapon = weaponEntity });
        
        private void CreateBullet(IEntityFactory entityFactory, ObjectId shootTypeId, Vector2 spawnPosition,
            Vector2 force)
        {
            var entity = entityFactory.CreateEntity(World);
            var rotation = MathExtensions.CalculateRotationFromVelocity(force);
            World.SendMessage(new SpawnRequest(entity, shootTypeId, spawnPosition, rotation));
#if UNITY_WEBGL
            World.SendMessage(new ForceRequest { Value = force, Entity = entity });
#else
            World.SendMessage(new ForceRequest { Value = force, EntityId = entity.ID });
#endif
        }

        public void Dispose()
        {
        }
    }
}