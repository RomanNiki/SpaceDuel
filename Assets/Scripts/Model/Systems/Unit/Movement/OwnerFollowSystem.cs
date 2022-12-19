﻿using Leopotam.Ecs;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents;
using Model.Components.Weapons;

namespace Model.Systems.Unit.Movement
{
    public class FollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Follower, Position, PlayerOwner>.Exclude<Velocity> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var owner = ref _filter.Get3(i);
                if (owner.Owner.IsAlive() == false) continue;
                ref var ownerPosition = ref owner.Owner.Get<Position>();
                ref var offset = ref _filter.Get1(i);
                ref var followerPosition = ref _filter.Get2(i);
                followerPosition.Value = ownerPosition.Value + offset.Offset;
            }
        }
    }
}