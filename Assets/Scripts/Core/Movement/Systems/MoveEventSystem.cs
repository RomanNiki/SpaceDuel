using System.Collections.Generic;
using Core.Characteristics.EnergyLimits.Components;
using Core.Common;
using Core.Common.Enums;
using Core.Extensions;
using Core.Input.Components;
using Core.Movement.Components.Events;
using Scellecs.Morpeh;

namespace Core.Movement.Systems
{
    public class MoveEventSystem : IFixedSystem
    {
        private Filter _filter;
        private Stash<InputMoveData> _inputMoveDataPool;
        private Filter _stopFilter;
        private readonly HashSet<TeamEnum> _movingSet = new();
        private readonly HashSet<TeamEnum> _rotationSet = new();
        private Stash<Team> _teamPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<InputMoveData>().With<Team>().Without<NoEnergyBlock>().Build();
            _stopFilter = World.Filter.With<InputMoveData>().With<Team>().With<NoEnergyBlock>().Build();
            _inputMoveDataPool = World.GetStash<InputMoveData>();
            _teamPool = World.GetStash<Team>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var input = ref _inputMoveDataPool.Get(entity);
                ref var team = ref _teamPool.Get(entity).Value;
                HandleMoveEvents(input, team, entity);
                HandleRotationEvent(input, team, entity);
            }

            foreach (var entity in _stopFilter)
            {
                ref var team = ref _teamPool.Get(entity).Value;
                World.SendMessage(new StopAccelerationEvent() { Entity = entity });
                World.SendMessage(new StopRotationEvent() { Entity = entity });
                if (_movingSet.Contains(team))
                {
                    _movingSet.Remove(team);
                }

                if (_rotationSet.Contains(team))
                {
                    _rotationSet.Remove(team);
                }
            }
        }

        private void HandleRotationEvent(InputMoveData input, TeamEnum team, Entity entity)
        {
            if (input.Rotation > 0.1f)
            {
                if (_rotationSet.Contains(team) == false)
                {
                    _rotationSet.Add(team);
                    World.SendMessage(new StartRotationEvent { Entity = entity });
                }
            }
            else
            {
                if (_rotationSet.Contains(team))
                {
                    _rotationSet.Remove(team);
                    World.SendMessage(new StopRotationEvent { Entity = entity });
                }
            }
        }

        private void HandleMoveEvents(InputMoveData input, TeamEnum team, Entity entity)
        {
            if (input.Accelerate)
            {
                if (_movingSet.Contains(team) == false)
                {
                    _movingSet.Add(team);
                    World.SendMessage(new StartAccelerationEvent { Entity = entity });
                }
            }
            else
            {
                if (_movingSet.Contains(team))
                {
                    _movingSet.Remove(team);
                    World.SendMessage(new StopAccelerationEvent { Entity = entity });
                }
            }
        }

        public void Dispose()
        {
            _movingSet.Clear();
            _rotationSet.Clear();
        }
    }
}