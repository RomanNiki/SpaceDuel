using _Project.Develop.Runtime.Core.Buffs.Components;
using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Core.Timers.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Buffs.Systems
{
    public sealed class SpawnBuffSystem : ISystem
    {
        private Filter _filter;
        private Stash<BuffSpawner> _spawnerPool;
        private Stash<Radius> _radiusPool;
        private Stash<Position> _positionPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<BuffSpawner>().With<Radius>().With<Position>()
                .Without<Timer<TimerBetweenSpawn>>().Build();
            _spawnerPool = World.GetStash<BuffSpawner>();
            _radiusPool = World.GetStash<Radius>();
            _positionPool = World.GetStash<Position>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
            }
        }

        public void Dispose()
        {
        }
    }
}