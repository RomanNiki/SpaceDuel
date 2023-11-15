using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Characteristics.Damage.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class KillWithoutHealthSystem : ISystem
    {
        private Stash<DeadTag> _deadTagPool;
        private Filter _killWithoutHealthFilter;
        private Stash<KillSelfRequest> _killSelfPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _killWithoutHealthFilter = World.Filter.With<KillSelfRequest>().Without<Health>().Without<DeadTag>().Build();
            _deadTagPool = World.GetStash<DeadTag>();
            _killSelfPool = World.GetStash<KillSelfRequest>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _killWithoutHealthFilter)
            {
                _deadTagPool.Add(entity);
                _killSelfPool.Remove(entity);
            }
        }
        
        public void Dispose()
        {
        }
    }
}