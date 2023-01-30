using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Extensions;
using Model.Extensions.EntityFactories;
using Model.Unit.EnergySystems.Components;
using Model.Unit.Movement.Components;
using Model.Weapons.Components;
using Model.Weapons.Components.Events;
using UnityEngine;

namespace Model.Weapons
{
    public sealed class ExecuteShootSystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<EntityFactoryRef<IEntityFactory>, Shooting, PlayerOwner, BulletStartForce, Muzzle>.Exclude<
                NoEnergyBlock> _readyToShotFilter = null;


        protected override void Tick()
        {
            foreach (var i in _readyToShotFilter)
            {
                ref var owner = ref _readyToShotFilter.Get3(i).Owner;
                ref var factory = ref _readyToShotFilter.Get1(i).Value;
                ref var bulletForce = ref _readyToShotFilter.Get4(i).Value;
                ref var offset = ref _readyToShotFilter.Get5(i).Offset;
                ref var direction = ref _readyToShotFilter.Get2(i).Direction;
                ref var playerPosition = ref owner.Get<Position>();
                ref var playerRotation = ref owner.Get<Rotation>();
                var spawnPosition = playerPosition.Value + (Vector2) playerRotation.LookDir * offset;

                CreateBullet(factory, spawnPosition, direction * bulletForce);

                ref var gun = ref _readyToShotFilter.GetEntity(i);
                MessageShotMade(gun);
            }
        }

        private void CreateBullet(IEntityFactory factory, in Vector2 position, in Vector2 velocity)
        {
            var entity = factory.CreateEntity(_world);
            entity.Get<Velocity>().Value = velocity;
            entity.AddTransform(position, Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
            entity.Get<ViewCreateRequest>().StartPosition = position;
        }

        private static void MessageShotMade(in EcsEntity gun) => gun.Get<ShotMadeEvent>();
    }
}