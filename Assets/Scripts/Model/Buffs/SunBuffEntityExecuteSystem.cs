using System;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Extensions;
using Model.Extensions.EntityFactories;
using Model.Timers.Components;
using Model.Unit.Movement.Components;
using Model.Unit.SunEntity.Components;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Model.Buffs
{
    public sealed class SunBuffEntityExecuteSystem : IEcsRunSystem
    {
        private readonly Settings _settings;
        private readonly EcsWorld _world;
        private readonly EcsFilter<Sun, Position, EntityFactoryRef<IEntityFactory>>.Exclude<Timer<TimerBetweenSpawn>> _filter;

        public SunBuffEntityExecuteSystem(Settings settings)
        {
            _settings = settings;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var sun = ref _filter.Get1(i);
                ref var position = ref _filter.Get2(i);
                ref var factory = ref _filter.Get3(i);
                ref var sunEntity = ref _filter.GetEntity(i);

                var angle = Random.Range(0, 2f * Mathf.PI);
                var dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                var ratio = sun.InnerRadius / sun.OuterRadius ;
                var radius = Mathf.Sqrt(Random.Range(ratio * ratio, 1f)) * sun.OuterRadius - 5f;
                var point = dir * radius;
                var spawnPoint = position.Value + point;
                CreateBuff(factory, spawnPoint);
                SetTimer(sunEntity);
            }
        }

        private void SetTimer(in EcsEntity entity)
        {
            entity.Get<Timer<TimerBetweenSpawn>>().TimeLeftSec =
                Random.Range(_settings.MinTimeToSpawn, _settings.MaxTimeToSpawn);
        }

        private void CreateBuff(in EntityFactoryRef<IEntityFactory> factory, Vector2 spawnPoint)
        {
            var entity = factory.Value.CreateEntity(_world);
            entity.AddTransform(spawnPoint);
            entity.Get<ViewCreateRequest>().StartPosition = spawnPoint;
        }

        [Serializable]
        public class Settings
        {
            public float MaxTimeToSpawn;
            public float MinTimeToSpawn;
        }
    }
}