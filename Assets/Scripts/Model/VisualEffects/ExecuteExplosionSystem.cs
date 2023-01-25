using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Extensions;
using Model.Extensions.EntityFactories;
using Model.Unit.Destroy.Components.Requests;
using Model.Unit.Movement.Components;
using Model.VisualEffects.Components.Events;
using Model.VisualEffects.Components.Tags;
using UnityEngine;
using Zenject;

namespace Model.VisualEffects
{
    public sealed class ExecuteExplosionSystem : IEcsRunSystem
    {
        [Inject] private readonly IEntityFactory _factory;
        private readonly EcsWorld _world;
        private readonly EcsFilter<Position, EntityDestroyRequest, ExplosiveTag> _filterExplosive = null;

        public void Run()
        {
            foreach (var i in _filterExplosive)
            {
                ref var explosionPosition = ref _filterExplosive.Get1(i).Value;
                CreateExplosion(_world, explosionPosition);
                _world.SendMessage(new ExplosionEvent(){Position = explosionPosition});
            }
        }

        private void CreateExplosion(EcsWorld world, Vector2 explosionPosition)
        {
            var explosion = _factory.CreateEntity(world);
            explosion.Get<ExplosionTag>();
            explosion.AddTransform(explosionPosition);
            explosion.Get<ViewCreateRequest>().StartPosition = explosionPosition;
        }
    }
}