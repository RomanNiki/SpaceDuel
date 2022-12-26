using Leopotam.Ecs;
using Model.Components.Extensions;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Model.Systems.Unit.Movement
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