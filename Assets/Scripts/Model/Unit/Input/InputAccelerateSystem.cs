using Leopotam.Ecs;
using Model.Enums;
using Model.Unit.Input.Components;
using Model.Unit.Input.Components.Events;
using Model.Unit.Movement.Components.Tags;

namespace Model.Unit.Input
{
    public sealed class InputAccelerateSystem :  IEcsRunSystem
    {
        private readonly EcsFilter<InputAccelerateStartedEvent> _accelerationStartFilter = null;
        private readonly EcsFilter<InputAccelerateCanceledEvent> _accelerationCanceledFilter = null;
        private readonly EcsFilter<PlayerTag, InputMoveData, Team> _moveFilter = null;
        
        private bool IsPlayerWithNumber(in TeamEnum playerTeamEnum, in int indexFilter)
        {
            var teamData = _moveFilter.Get3(indexFilter);
            return teamData.Value == playerTeamEnum;
        }

        private void ProcessMove(in TeamEnum numberPlayer, in bool doMove)
        {
            foreach (var i in _moveFilter)
            {
                if (IsPlayerWithNumber(numberPlayer, i) == false)
                    continue;
                ref var move = ref _moveFilter.Get2(i);

                MovePlayer(ref move, doMove);
            }
        }

        private static void MovePlayer(ref InputMoveData inputMove, bool doMove)
        {
            inputMove.Accelerate = doMove;
        }

        public void Run()
        {
            foreach (var i in _accelerationStartFilter)
            {
                ref var inputMoveStartedEvent = ref _accelerationStartFilter.Get1(i);
                ProcessMove(inputMoveStartedEvent.PlayerTeam, true);
            }
            
            foreach (var i in _accelerationCanceledFilter)
            {
                ref var inputMoveCanceledEvent = ref _accelerationCanceledFilter.Get1(i);
                ProcessMove(inputMoveCanceledEvent.PlayerTeam, false);
            }
        }
    }
}