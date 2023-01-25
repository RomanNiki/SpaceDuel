using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.Movement.Components;
using UnityEngine;

namespace Model.Unit.Movement
{
    public sealed class VelocitySystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<Position, Velocity> _move = null;

        protected override void Tick()
        {
            foreach (var i in _move)
            {
                ref var position = ref _move.Get1(i);
                ref var move = ref _move.Get2(i);
                position.Value += move.Value * Time.deltaTime;
            }
        }
    }
}