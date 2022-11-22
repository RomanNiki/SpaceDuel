﻿using Components.Unit.MoveComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Unit.Movement
{
    public sealed class VelocitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<Move> _move = null;

        public void Run()
        {
            foreach (var i in _move)
            {
                ref var move = ref _move.Get1(i);
                move.Position += move.Velocity * Time.fixedDeltaTime;
            }
        }
    }
}