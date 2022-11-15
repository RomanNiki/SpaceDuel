using Components.Player.MoveComponents;
using Events.InputEvents;
using Leopotam.Ecs;
using Models.Player;
using Tags;

namespace Systems.Player.Movement
{
    public sealed class InputAccelerateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputAccelerateEvent> _filterAccelerationStart = null;
        private readonly EcsFilter<InputAccelerateCanceledEvent> _filterAccelerationCanceled = null;
        private readonly EcsFilter<PlayerTag, InputData> _filterMove = null;

        public void Run()
        {
            foreach (var i in _filterAccelerationStart)
            {
                ref var inputMoveStartedEvent = ref _filterAccelerationStart.Get1(i);
                ProcessMove(inputMoveStartedEvent.PlayerNumber, true);
            }

            foreach (var i in _filterAccelerationCanceled)
            {
                ref var inputMoveCanceledEvent = ref _filterAccelerationCanceled.Get1(i);
                ProcessMove(inputMoveCanceledEvent.PlayerNumber, false);
            }
        }

        private bool IsPlayerWithNumber(in Team playerTeam, in int indexFilter)
        {
            var inputData = _filterMove.Get2(indexFilter);
            return inputData.Team == playerTeam;
        }

        private void ProcessMove(in Team numberPlayer, in bool doMove)
        {
            foreach (var i in _filterMove)
            {
                if (IsPlayerWithNumber(numberPlayer, i) == false)
                    continue;
                ref var move = ref _filterMove.Get2(i);
                
                MovePlayer(ref move, doMove);
            }
        }

        private static void MovePlayer(ref InputData move, bool doMove)
        {
            move.Accelerate = doMove;
        }
    }
}