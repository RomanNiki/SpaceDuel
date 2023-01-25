using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.Movement.Components;
using UnityEngine;

namespace Model.Unit.Movement
{
    public sealed class FrictionSystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<Velocity, Friction> _moveFilter = null;

        protected override void Tick()
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