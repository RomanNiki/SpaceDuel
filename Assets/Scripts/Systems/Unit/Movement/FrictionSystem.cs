using System;
using Components.Unit.MoveComponents;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Systems.Unit.Movement
{
    public class FrictionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Move> _move = null;
        [Inject] private readonly Settings _settings;

        public void Run()
        {
            foreach (var i in _move)
            {
                ref var move = ref _move.Get1(i);
                var frictionDirection = -move.Velocity.normalized;
                var frictionMagnitude = move.Velocity.magnitude * move.Velocity.magnitude;
                var frictionVector = frictionDirection * frictionMagnitude;
                move.Velocity += _settings.FrictionCoefficient * frictionVector * Time.fixedDeltaTime;
            }
        }

        [Serializable]
        public class Settings
        {
            public float FrictionCoefficient;
        }
    }
}