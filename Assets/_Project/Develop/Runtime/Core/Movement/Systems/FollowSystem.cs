using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Core.Weapon.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Movement.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class FollowSystem : IFixedSystem
    {
        private Filter _filter;
        private Stash<Position> _positionPool;
        private Stash<Owner> _ownerPool;
        private Stash<Follower> _followerPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<Follower>().With<Position>().With<Owner>().Build();
            _positionPool = World.GetStash<Position>();
            _followerPool = World.GetStash<Follower>();
            _ownerPool = World.GetStash<Owner>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _ownerPool.Get(entity);
                if (owner.Entity.IsNullOrDisposed()) continue;
                ref var ownerPosition = ref _positionPool.Get(owner.Entity);
                ref var offset = ref _followerPool.Get(entity);
                ref var followerPosition = ref _positionPool.Get(entity);
                followerPosition.Value = ownerPosition.Value + offset.Offset;
            }
        }

        public void Dispose()
        {
        }
    }
}