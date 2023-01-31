using Leopotam.Ecs;
using Model.Extensions;
using Model.Unit.Movement.Components;
using Model.Weapons.Components.Tags;
using UnityEngine;

namespace Model.Unit.Movement
{
    public sealed class RotateToVelocitySystem : PauseHandlerDefaultRunSystem
    {
        private readonly EcsFilter<BulletTag, Rotation, Velocity> _bulletsFilter = null;
        
        protected override void Tick()
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