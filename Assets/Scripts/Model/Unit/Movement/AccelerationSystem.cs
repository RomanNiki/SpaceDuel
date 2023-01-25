using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.Movement.Components;
using UnityEngine;

namespace Model.Unit.Movement
{
    public sealed class AccelerationSystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<Velocity, ForceRequest> _moveFilter = null;

        protected override void Tick()
        {
            foreach (var i in _moveFilter)
            {
                ref var move = ref _moveFilter.Get1(i);
                ref var force = ref _moveFilter.Get2(i);
                move.Value += force.Force * Time.deltaTime;
            }
        }
    }
}