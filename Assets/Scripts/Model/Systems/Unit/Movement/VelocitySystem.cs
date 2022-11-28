using Leopotam.Ecs;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Model.Systems.Unit.Movement
{
    public sealed class VelocitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformData, Move> _move = null;

        public void Run()
        {
            foreach (var i in _move)
            {
                ref var transform = ref _move.Get1(i);
                ref var move = ref _move.Get2(i);
                transform.Position += move.Velocity * Time.deltaTime;
            }
        }
    }
}