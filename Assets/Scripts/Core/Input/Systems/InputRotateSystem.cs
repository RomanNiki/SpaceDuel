using Core.Enums;
using Core.Input.Components;
using Core.Player.Components;
using Scellecs.Morpeh;

namespace Core.Input.Systems
{
    public sealed class InputRotateSystem : ISystem
    {
        private Filter _rotationStartedFilter;
        private Filter _rotationCanceledFilter;
        private Filter _inputDataFilter;
        private Stash<InputRotateStartedEvent> _rotationStartedPool;
        private Stash<InputRotateCanceledEvent> _rotationCanceledPool;
        private Stash<Team> _teamPool;
        private Stash<InputMoveData> _inputDataPool;
        
        public World World { get; set; }
        
        public void OnAwake()
        {
            _rotationStartedPool = World.GetStash<InputRotateStartedEvent>();
            _rotationCanceledPool = World.GetStash<InputRotateCanceledEvent>();
            _teamPool = World.GetStash<Team>();
            _inputDataPool = World.GetStash<InputMoveData>();
            _rotationStartedFilter = World.Filter.With<InputRotateStartedEvent>();
            _rotationCanceledFilter = World.Filter.With<InputRotateCanceledEvent>();
            _inputDataFilter = World.Filter.With<PlayerTag>().With<InputMoveData>().With<Team>();
        }

        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _rotationStartedFilter)
            {
                ref var inputRotateStarted = ref _rotationStartedPool.Get(entity);
                if (inputRotateStarted.Axis == 0) continue;
                var direction = inputRotateStarted.Axis > 0 ? -1 : 1;
                ProcessRotation(inputRotateStarted.PlayerTeam, direction);
            }

            foreach (var entity in _rotationCanceledFilter)
            {
                ref var inputRotateCanceledEvent = ref _rotationCanceledPool.Get(entity);
                ProcessRotation(inputRotateCanceledEvent.PlayerNumber, 0f);
            }
        }

        private void ProcessRotation(TeamEnum playerTeam, in float angle)
        {
            foreach (var inputEntity in _inputDataFilter)
            {
                if (_teamPool.Get(inputEntity).Value != playerTeam)
                    continue;
                _inputDataPool.Get(inputEntity).Rotation = angle;
            }
        }

        public void Dispose()
        {
        }
    }
}