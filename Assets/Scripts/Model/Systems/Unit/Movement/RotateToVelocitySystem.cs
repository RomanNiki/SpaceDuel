using Leopotam.Ecs;
using Model.Components.Tags;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Model.Systems.Unit.Movement
{
    public sealed class RotateToVelocitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<BulletTag, TransformData, Move> _bulletsFilter = null;

        public void Run()
        {
            foreach (var i in _bulletsFilter)
            {
                ref var transform = ref _bulletsFilter.Get2(i);
                ref var move = ref _bulletsFilter.Get3(i);
                transform.Rotation = Mathf.Atan2(move.Velocity.y, move.Velocity.x) * Mathf.Rad2Deg + 90f;
            }
        }
    }
}