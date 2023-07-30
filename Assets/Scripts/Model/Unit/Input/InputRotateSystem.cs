using Leopotam.Ecs;
using Model.Enums;
using Model.Extensions;
using Model.Unit.Input.Components;
using Model.Unit.Input.Components.Events;
using Model.Unit.Movement.Components.Tags;

namespace Model.Unit.Input
{
    public sealed class InputRotateSystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<InputRotateStartedEvent> _rotationStartFilter = null;
        private readonly EcsFilter<InputRotateCanceledEvent> _rotationCanceledFilter = null;
        private readonly EcsFilter<PlayerTag, InputMoveData, Team> _moveFilter = null;

        protected override void Tick()
        {
            foreach (var i in _rotationStartFilter)
            {
                ref var inputMoveStartedEvent = ref _rotationStartFilter.Get1(i);
                var direction = 0;
                if (inputMoveStartedEvent.Axis != 0)
                {
                    direction = inputMoveStartedEvent.Axis > 0 ? 1 : -1;
                }
                ProcessRotation(inputMoveStartedEvent.PlayerTeam, direction);
            }

            foreach (var i in _rotationCanceledFilter)
            {
                ref var inputMoveCanceledEvent = ref _rotationCanceledFilter.Get1(i);
                ProcessRotation(inputMoveCanceledEvent.PlayerTeam, 0f);
            }
        }

        private bool IsPlayerWithNumber(in TeamEnum playerTeamEnum, in int indexFilter)
        {
            var teamData = _moveFilter.Get3(indexFilter);
            return teamData.Value == playerTeamEnum;
        }

        private void ProcessRotation(in TeamEnum numberPlayer, in float angle)
        {
            foreach (var i in _moveFilter)
            {
                if (IsPlayerWithNumber(numberPlayer, i) == false)
                    continue;
                ref var rotate = ref _moveFilter.Get2(i);
                
                RotatePlayer(ref rotate, angle);
            }
        }

        private static void RotatePlayer(ref InputMoveData inputMove, float angle)
        {
            inputMove.Rotation = angle;
        }
    }
}