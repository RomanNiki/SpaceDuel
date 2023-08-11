using Core.Movement.Components;
using Scellecs.Morpeh;

namespace Core.Movement.Systems
{
    public sealed class MoveClampSystem : IFixedSystem
    {
        private readonly IMoveLoopService _loopService;
        private Stash<Position> _positionPool;
        private Filter _filter;
        
        public World World { get; set; }
        
        public MoveClampSystem(IMoveLoopService loopService)
        {
            _loopService = loopService;
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<Position>();
            _positionPool = World.GetStash<Position>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var position = ref _positionPool.Get(entity);
                position.Value = _loopService.LoopPosition(position.Value);
            }
        }

        public void Dispose()
        {
        }
    }
}