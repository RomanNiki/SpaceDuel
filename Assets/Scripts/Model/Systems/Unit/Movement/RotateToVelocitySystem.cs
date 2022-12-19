using Leopotam.Ecs;
using Model.Components.Tags;
using Model.Components.Tags.Projectiles;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Model.Systems.Unit.Movement
{
    public sealed class RotateToVelocitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<BulletTag, Rotation, Velocity> _bulletsFilter = null;

        public void Run()
        {
            foreach (var i in _bulletsFilter)
            {
                ref var rotation = ref _bulletsFilter.Get2(i);
                ref var move = ref _bulletsFilter.Get3(i);
                rotation.Value = Mathf.Atan2(move.Value.y, move.Value.x) * Mathf.Rad2Deg + 90f;
            }
        }
    }
}