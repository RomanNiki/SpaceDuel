using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.Movement.Components;
using UnityEngine;

namespace Model.Unit.Movement
{
    public sealed class VelocitySystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<Position, Velocity> _moveFilter = null;

        protected override void Tick()
        {
            foreach (var i in _moveFilter)
            {
                ref var position = ref _moveFilter.Get1(i);
                ref var move = ref _moveFilter.Get2(i);
                position.Value += move.Value * Time.deltaTime;
            }
        }
    }
}