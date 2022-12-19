using System;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Extensions;
using Model.Components.Extensions.EntityFactories;
using Model.Components.Unit.MoveComponents;
using Model.Timers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Model.Systems
{
    public class SunBuffEntityExecuteSystem : IEcsRunSystem, IEcsInitSystem
    {
        [Inject] private Settings _settings;
        private EcsWorld _world;
        private EcsFilter<Sun, Position, EntityFactoryRef<IEntityFactory>>.Exclude<Timer<TimerBetweenSpawn>> _filter;

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
                var ratio = sun.InnerRadius / sun.OuterRadius;
                var radius = Mathf.Sqrt(Random.Range(ratio * ratio, 1f)) * sun.OuterRadius;
                var point = dir * radius;
                var spawnPoint = position.Value + point;
                CreateBuff(factory, spawnPoint);
                SetTimer(sunEntity);
            }
        }

        public void Init()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                SetTimer(entity);
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
        }

        [Serializable]
        public class Settings
        {
            public float MaxTimeToSpawn;
            public float MinTimeToSpawn;
        }
    }
}