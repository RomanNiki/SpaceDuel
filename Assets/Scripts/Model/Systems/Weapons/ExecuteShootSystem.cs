using Leopotam.Ecs;
using Model.Components;
using Model.Components.Events;
using Model.Components.Extensions;
using Model.Components.Extensions.EntityFactories;
using Model.Components.Requests;
using Model.Components.Unit.MoveComponents;
using Model.Components.Weapons;
using UnityEngine;

namespace Model.Systems.Weapons
{
    public sealed class ExecuteShootSystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly
            EcsFilter<EntityFactoryRef<IEntityFactory>, Shooting, PlayerOwner, BulletStartForce, Muzzle>.Exclude<
                NoEnergyBlock> _filter = null;


        protected override void Tick()
        {
            foreach (var i in _filter)
            {
                ref var owner = ref _filter.Get3(i).Owner;
                ref var factory = ref _filter.Get1(i).Value;
                ref var bulletForce = ref _filter.Get4(i).Value;
                ref var offset = ref _filter.Get5(i).Offset;
                ref var direction = ref _filter.Get2(i).Direction;
                ref var playerPosition = ref owner.Get<Position>();
                ref var playerRotation = ref owner.Get<Rotation>();
                var spawnPosition = playerPosition.Value + (Vector2) playerRotation.LookDir * offset;

                CreateBullet(factory, spawnPosition, direction * bulletForce);

                ref var gun = ref _filter.GetEntity(i);
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