using System;
using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Components.Tags;
using Model.Components.Unit.MoveComponents;
using Model.Timers;
using UnityEngine;
using Zenject;

namespace Model.Systems
{
    public sealed class EntityExplosionSystem : IEcsRunSystem
    {
        [Inject] private readonly Settings _settings;
        private readonly EcsWorld _world;
        private readonly EcsFilter<TransformData, EntityDestroyRequest, ExplosiveTag> _filterExplosive = null;

        public void Run()
        {
            foreach (var i in _filterExplosive)
            {
                ref var explosionPosition = ref _filterExplosive.Get1(i).Position;
                CreateExplosion(_world, explosionPosition);
            }
        }

        private void CreateExplosion(EcsWorld world, Vector2 explosionPosition)
        {
            var explosion = world.NewEntity();
            explosion.Get<ExplosionTag>();
            explosion.Get<NoGravity>();
            explosion.Get<TransformData>().Position = explosionPosition;
            explosion.Get<ViewCreateRequest>().StartPosition = explosionPosition;
            explosion.Get<Timer<DestroyTimer>>().TimeLeftSec = _settings.ExplosionLifeTime;
        }
        
        [Serializable]
        public class Settings
        {
            public float ExplosionLifeTime;
        }
    }
}