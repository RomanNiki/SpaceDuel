using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Movement.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Characteristics.Damage.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class InstantlyKillSystem : ISystem
    {
        private Filter _requestsFilter;
        private Stash<DeadTag> _deadTagPool;
        private Stash<KillRequest> _killPool;
        private Stash<Health> _healthPool;
        private Stash<Position> _positionPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _requestsFilter = World.Filter.With<KillRequest>().Build();
            _deadTagPool = World.GetStash<DeadTag>();
            _healthPool = World.GetStash<Health>();
            _killPool = World.GetStash<KillRequest>();
            _positionPool = World.GetStash<Position>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var request in _requestsFilter)
            {
                ref var entityToKill = ref _killPool.Get(request).EntityToKill;
                if (entityToKill.IsNullOrDisposed())
                    continue;
                if (_deadTagPool.Has(entityToKill))
                    continue;

                if (_healthPool.Has(entityToKill))
                {
                    World.SendMessage(new DamageRequest(_healthPool.Get(entityToKill).Value,
                        _positionPool.Get(entityToKill).Value, entityToKill));
                }
                else
                {
                    _deadTagPool.Add(entityToKill);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}