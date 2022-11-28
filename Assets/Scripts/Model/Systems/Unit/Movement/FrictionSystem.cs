using Leopotam.Ecs;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Model.Systems.Unit.Movement
{
    public sealed class FrictionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Move, Friction> _moveFilter = null;

        public void Run()
        {
            foreach (var i in _moveFilter)
            {
                ref var move = ref _moveFilter.Get1(i);
                ref var friction = ref _moveFilter.Get2(i);
                var frictionDirection = -move.Velocity.normalized;
                var frictionMagnitude = move.Velocity.magnitude * move.Velocity.magnitude;
                var frictionVector = frictionDirection * frictionMagnitude;
                move.Velocity += friction.Value * frictionVector * Time.deltaTime;
            }
        }
    }
}