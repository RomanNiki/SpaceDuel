using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Extensions;
using Model.Components.Extensions.EntityFactories;
using Model.Components.Requests;
using Model.Components.Tags;
using Model.Components.Tags.Effects;
using Model.Components.Unit.MoveComponents;
using UnityEngine;
using Zenject;

namespace Model.Systems.VisualEffects
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
                _world.SendMessage(new ExplosionEvent());
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