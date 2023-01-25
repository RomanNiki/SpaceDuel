using System.Linq;
using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Extensions;
using Model.Extensions.EntityFactories;
using Model.Unit.Collisions.Components.Events;
using Model.Unit.Damage.Components;
using Model.Unit.Movement.Components;
using Model.VisualEffects.Components.Tags;
using Model.Weapons.Components.Tags;
using UnityEngine;
using Zenject;

namespace Model.VisualEffects
{
    public sealed class ExecuteHitSystem : IEcsRunSystem
    {
        [Inject] private readonly IEntityFactory _factory;
        private readonly EcsWorld _world;
        private readonly EcsFilter<ContainerComponents<TriggerEnterEvent>, Position, Rotation, BulletTag> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var collisions = ref _filter.Get1(i);
                if (collisions.List.Any(collision => collision.Other.Has<Health>() && collision.Other.IsAlive()))
                {
                    var hitPosition = _filter.Get2(i);
                    var hitRotation = _filter.Get3(i);
                    CreateHit(_world, hitPosition.Value, hitRotation.Value);
                }
            }
        }

        private void CreateHit(EcsWorld world, Vector2 hitPosition, float hitRotation)
        {
            var hitEntity = _factory.CreateEntity(world);
            hitEntity.Get<HitTag>();
            hitEntity.AddTransform(hitPosition, hitRotation);
            hitEntity.Get<ViewCreateRequest>().StartPosition = hitPosition;
        }
    }
}