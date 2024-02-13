using System.Collections.Generic;
using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Common;
using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Input.Components;
using _Project.Develop.Runtime.Core.Movement.Components.Events;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Movement.Systems
{
    public class MoveEventSystem : IFixedSystem
    {
        private Filter _filter;
        private Stash<InputMoveData> _inputMoveDataPool;
        private Filter _stopFilter;
        private readonly HashSet<TeamEnum> _movingSet = new();
        private readonly HashSet<TeamEnum> _rotationSet = new();
        private Stash<Team> _teamPool;
        private Stash<Energy> _energyPool;
        public World World { get; set; }
        private const float RotationThreshold = 0.1f;

        public void OnAwake()
        {
            _filter = World.Filter.With<InputMoveData>().With<Team>().With<Energy>().Build();
            _inputMoveDataPool = World.GetStash<InputMoveData>();
            _teamPool = World.GetStash<Team>();
            _energyPool = World.GetStash<Energy>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var input = ref _inputMoveDataPool.Get(entity);
                ref var team = ref _teamPool.Get(entity).Value;
                
                if (_energyPool.Get(entity).HasEnergy)
                {
                    HandleMoveEvents(input, team, entity);
                    HandleRotationEvent(input, team, entity);
                }
                else
                {
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
        }

        private void HandleRotationEvent(InputMoveData input, TeamEnum team, Entity entity)
        {
            if (Mathf.Abs(input.Rotation) > RotationThreshold)
            {
                if (_rotationSet.Add(team))
                {
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
                if (_movingSet.Add(team))
                {
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