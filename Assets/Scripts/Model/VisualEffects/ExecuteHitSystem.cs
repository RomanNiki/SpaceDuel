using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Extensions;
using Model.Extensions.EntityFactories;
using Model.Unit.Collisions.Components.Events;
using Model.Unit.Movement.Components;
using Model.VisualEffects.Components.Tags;
using Model.Weapons.Components.Tags;
using UnityEngine;

namespace Model.VisualEffects
{
    public sealed class ExecuteHitSystem : IEcsRunSystem
    {
        private readonly IEntityFactory _entityFactory;
        private readonly EcsWorld _world;
        private readonly EcsFilter<ContainerComponents<TriggerEnterEvent>, Position, Rotation, BulletTag> _filter;

        public ExecuteHitSystem(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var collisions = _filter.Get1(i).List;
                foreach (var collision in collisions)
                {
                    var hitPosition = _filter.Get2(i);
                    var hitRotation = _filter.Get3(i);
                    CreateHit(_world, hitPosition.Value, hitRotation.Value);
                }
            }
        }

        private void CreateHit(EcsWorld world, Vector2 hitPosition, float hitRotation)
        {
            var hitEntity = _entityFactory.CreateEntity(world);
            hitEntity.Get<HitTag>();
            hitEntity.AddTransform(hitPosition, hitRotation);
            hitEntity.Get<ViewCreateRequest>().StartPosition = hitPosition;
        }
    }
}