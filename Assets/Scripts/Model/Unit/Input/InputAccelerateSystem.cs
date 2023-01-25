using Leopotam.Ecs;
using Model.Enums;
using Model.Extensions;
using Model.Unit.Input.Components;
using Model.Unit.Input.Components.Events;
using Model.Unit.Movement.Components.Tags;

namespace Model.Unit.Input
{
    public sealed class InputAccelerateSystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<InputAccelerateEvent> _filterAccelerationStart = null;
        private readonly EcsFilter<InputAccelerateCanceledEvent> _filterAccelerationCanceled = null;
        private readonly EcsFilter<PlayerTag, InputMoveData, Team> _filterMove = null;
        
        protected override void Tick()
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

        private bool IsPlayerWithNumber(in TeamEnum playerTeamEnum, in int indexFilter)
        {
            var teamData = _filterMove.Get3(indexFilter);
            return teamData.Value == playerTeamEnum;
        }

        private void ProcessMove(in TeamEnum numberPlayer, in bool doMove)
        {
            foreach (var i in _filterMove)
            {
                if (IsPlayerWithNumber(numberPlayer, i) == false)
                    continue;
                ref var move = ref _filterMove.Get2(i);
                
                MovePlayer(ref move, doMove);
            }
        }

        private static void MovePlayer(ref InputMoveData inputMove, bool doMove)
        {
            inputMove.Accelerate = doMove;
        }
    }
}