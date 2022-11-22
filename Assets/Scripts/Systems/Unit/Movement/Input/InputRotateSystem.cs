﻿using Components.Events.InputEvents;
using Components.Tags;
using Components.Unit.MoveComponents.Input;
using Enums;
using Events.InputEvents;
using Leopotam.Ecs;

namespace Systems.Unit.Movement.Input
{
    internal sealed class InputRotateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputRotateStartedEvent> _filterRotationStart = null;
        private readonly EcsFilter<InputRotateCanceledEvent> _filterRotationCanceled = null;
        private readonly EcsFilter<PlayerTag, InputMoveData, TeamData> _filterMove = null;

        public void Run()
        {
            foreach (var i in _filterRotationStart)
            {
                ref var inputMoveStartedEvent = ref _filterRotationStart.Get1(i);
                var direction = 0;
                if (inputMoveStartedEvent.Axis != 0)
                {
                    direction = inputMoveStartedEvent.Axis > 0 ? 1 : -1;
                }
                ProcessRotation(inputMoveStartedEvent.PlayerNumber, direction);
            }

            foreach (var i in _filterRotationCanceled)
            {
                ref var inputMoveCanceledEvent = ref _filterRotationCanceled.Get1(i);
                ProcessRotation(inputMoveCanceledEvent.PlayerNumber, 0f);
            }
        }
        
        private bool IsPlayerWithNumber(in Team playerTeam, in int indexFilter)
        {
            var teamData = _filterMove.Get3(indexFilter);
            return teamData.Team == playerTeam;
        }

        private void ProcessRotation(in Team numberPlayer, in float angle)
        {
            foreach (var i in _filterMove)
            {
                if (IsPlayerWithNumber(numberPlayer, i) == false)
                    continue;
                ref var rotate = ref _filterMove.Get2(i);
                
                RotatePlayer(ref rotate, angle);
            }
        }

        private static void RotatePlayer(ref InputMoveData inputMove, float angle)
        {
            inputMove.Rotation = angle;
        }
    }
}