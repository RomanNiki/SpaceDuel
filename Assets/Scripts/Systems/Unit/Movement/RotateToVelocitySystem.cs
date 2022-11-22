using Components.Tags;
using Components.Unit.MoveComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Unit.Movement
{
    internal sealed class RotateToVelocitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<BulletTag, Move> _bulletsFilter = null;

        public void Run()
        {
            foreach (var i in _bulletsFilter)
            {
                ref var move = ref _bulletsFilter.Get2(i);
                move.Rotation = Mathf.Atan2(move.Velocity.y, move.Velocity.x) * Mathf.Rad2Deg + 90f;
            }
        }
    }
}