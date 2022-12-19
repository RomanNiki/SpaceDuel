using Leopotam.Ecs;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Model.Systems.Unit.Movement
{
    public sealed class FrictionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Velocity, Friction> _moveFilter = null;

        public void Run()
        {
            foreach (var i in _moveFilter)
            {
                ref var move = ref _moveFilter.Get1(i);
                ref var friction = ref _moveFilter.Get2(i);
                var frictionDirection = -move.Value.normalized;
                var frictionMagnitude = move.Value.magnitude * move.Value.magnitude;
                var frictionVector = frictionDirection * frictionMagnitude;
                move.Value += friction.Value * frictionVector * Time.deltaTime;
            }
        }
    }
}