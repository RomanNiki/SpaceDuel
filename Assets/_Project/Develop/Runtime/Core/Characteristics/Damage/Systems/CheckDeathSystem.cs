using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Characteristics.Damage.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class CheckDeathSystem : ISystem
    {
        private Filter _healthFilter;
        private Stash<Health> _healthPool;
        private Stash<DeadTag> _deadTagPool;

        public World World { get; set; }

        public void OnAwake()
        {
            _healthFilter = World.Filter.With<Health>().Without<DeadTag>().Build();
            _healthPool = World.GetStash<Health>();
            _deadTagPool = World.GetStash<DeadTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _healthFilter)
            {
                ref var health = ref _healthPool.Get(entity);

                if (health.Value <= health.MinValue)
                {
                    _deadTagPool.Add(entity);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}