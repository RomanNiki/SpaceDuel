using Core.Characteristics.Enums;
using Core.Characteristics.Player.Components;
using Core.Input.Components;
using Scellecs.Morpeh;

namespace Core.Input.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public sealed class InputAccelerateSystem : ISystem
    {
        private Filter _accelerateStartedFilter;
        private Filter _accelerateCancelledFilter;
        private Stash<InputAccelerateStartedEvent> _accelerateStartedPool;
        private Stash<InputAccelerateCanceledEvent> _accelerateCanceledPool;
        public World World { get; set; }
        private Filter _playersFilter;
        private Stash<InputMoveData> _inputMoveDataPool;
        private Stash<Team> _teamPool;

        public void OnAwake()
        {
            _accelerateCanceledPool = World.GetStash<InputAccelerateCanceledEvent>();
            _accelerateStartedPool = World.GetStash<InputAccelerateStartedEvent>();
            _teamPool = World.GetStash<Team>();
            _inputMoveDataPool = World.GetStash<InputMoveData>();
            _accelerateCancelledFilter = World.Filter.With<InputAccelerateCanceledEvent>().Build();
            _accelerateStartedFilter = World.Filter.With<InputAccelerateStartedEvent>().Build();
            _playersFilter = World.Filter.With<PlayerTag>().With<Team>().With<InputMoveData>().Build();
        }
        
        private void ProcessMove(TeamEnum playerTeam, in bool doMove)
        {
            foreach (var entity in _playersFilter)
            {
                ref var team = ref _teamPool.Get(entity);
                if (team.Value != playerTeam) continue;
                ref var inputMoveData = ref _inputMoveDataPool.Get(entity);
                inputMoveData.Accelerate = doMove;
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _accelerateStartedFilter)
            {
                var team = _accelerateStartedPool.Get(entity).PlayerTeam;
                ProcessMove(team, true);
            }

            foreach (var entity in _accelerateCancelledFilter)
            {
                var team = _accelerateCanceledPool.Get(entity).PlayerTeam;
                ProcessMove(team, false);
            }
        }

        public void Dispose()
        {
        }
    }
}