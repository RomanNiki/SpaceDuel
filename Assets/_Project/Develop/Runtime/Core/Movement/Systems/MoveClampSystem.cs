using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Core.Services;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Movement.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class MoveClampSystem : IFixedSystem
    {
        private const float MAXIMUM_POSITION_DEVIATION = 0.1f;
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
            _filter = World.Filter.With<Position>().Build();
            _positionPool = World.GetStash<Position>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var position = ref _positionPool.Get(entity);
                var loopedPosition = _loopService.LoopPosition(position.Value);
                if (EqualPositions(loopedPosition ,position.Value) == false)
                {
                    position.Value = loopedPosition;
                }
            }
        }

        private static bool EqualPositions(Vector2 loopedPos, Vector2 position)
        {
            var x1 = loopedPos.x;
            var x2 = position.x;
            var y1 = loopedPos.y;
            var y2 = position.y;
            
            return Mathf.Abs(x1 - x2) <= MAXIMUM_POSITION_DEVIATION && Mathf.Abs(y1 - y2) <= MAXIMUM_POSITION_DEVIATION;
        }

        public void Dispose()
        {
        }
    }
}