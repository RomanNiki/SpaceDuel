using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Model.Systems.Unit.Movement
{
    public sealed class AccelerationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Move, ForceRequest> _moveFilter = null;
        
        public void Run()
        {
            foreach (var i in _moveFilter)
            {
                ref var move = ref _moveFilter.Get1(i);
                ref var force = ref _moveFilter.Get2(i);
                move.Velocity += force.Force * Time.deltaTime;
            }
        }
    }
}