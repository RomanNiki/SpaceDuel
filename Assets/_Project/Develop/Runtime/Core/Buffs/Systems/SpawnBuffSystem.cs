using _Project.Develop.Runtime.Core.Buffs.Components;
using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Core.Services.Random;
using _Project.Develop.Runtime.Core.Timers.Components;
using _Project.Develop.Runtime.Core.Views.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Buffs.Systems
{
    public sealed class SpawnBuffSystem : ISystem
    {
        private readonly IRandom _random;
        private Filter _filter;
        private Stash<BuffSpawner> _spawnerPool;
        private Stash<SpawnRadius> _radiusPool;
        private Stash<Position> _positionPool;
        private Stash<Timer<TimerBetweenSpawn>> _timerPool;
        public World World { get; set; }

        public SpawnBuffSystem(IRandom random)
        {
            _random = random;
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<BuffSpawner>().With<SpawnRadius>().With<Position>()
                .Without<Timer<TimerBetweenSpawn>>().Build();
            _spawnerPool = World.GetStash<BuffSpawner>();
            _radiusPool = World.GetStash<SpawnRadius>();
            _positionPool = World.GetStash<Position>();
            _timerPool = World.GetStash<Timer<TimerBetweenSpawn>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var position = ref _positionPool.Get(entity);
                ref var radius = ref _radiusPool.Get(entity);
        
                
                var angle = _random.Range(0, 2f * Mathf.PI);
                var dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                var ratio = radius.InnerRadius / radius.OuterRadius;
                var r = Mathf.Sqrt(_random.Range(ratio * ratio, 1f)) * radius.OuterRadius;
                var point = dir * r;
                var spawnPoint = position.Value + point;
                CreateBuff(spawnPoint);
                SetTimer(entity);
            }
        }

        private void SetTimer(Entity entity)
        {
            ref var spawner = ref _spawnerPool.Get(entity);
            _timerPool.Set(entity, new Timer<TimerBetweenSpawn>(spawner.SpawnIntervalSec));
        }


        private void CreateBuff(Vector2 spawnPoint)
        {
            var rotation = _random.Range(0f, 360f);
            World.SendMessage(new SpawnRequest(World.CreateEntity(), ObjectId.EnergyBuff, spawnPoint, rotation));
        }

        public void Dispose()
        {
        }
    }
}