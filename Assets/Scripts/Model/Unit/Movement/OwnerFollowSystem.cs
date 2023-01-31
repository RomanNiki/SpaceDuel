using Leopotam.Ecs;
using Model.Unit.Movement.Components;
using Model.Weapons.Components;

namespace Model.Unit.Movement
{
    public sealed class FollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Follower, Position, PlayerOwner>.Exclude<Velocity> _followFilter;

        public void Run()
        {
            foreach (var i in _followFilter)
            {
                ref var owner = ref _followFilter.Get3(i);
                if (owner.Owner.IsAlive() == false) continue;
                ref var ownerPosition = ref owner.Owner.Get<Position>();
                ref var offset = ref _followFilter.Get1(i);
                ref var followerPosition = ref _followFilter.Get2(i);
                followerPosition.Value = ownerPosition.Value + offset.Offset;
            }
        }
    }
}