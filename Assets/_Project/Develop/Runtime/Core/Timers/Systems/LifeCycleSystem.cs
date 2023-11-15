using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Timers.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Timers.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public sealed class LifeCycleSystem : ISystem
    {
        private Filter _filter;
        private Stash<KillSelfRequest> _killRequestPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<DieWithoutLifeTimerTag>().Without<Timer<LifeTimer>>().Build();
            _killRequestPool = World.GetStash<KillSelfRequest>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                if (_killRequestPool.Has(entity) == false)
                {
                    _killRequestPool.Add(entity);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}