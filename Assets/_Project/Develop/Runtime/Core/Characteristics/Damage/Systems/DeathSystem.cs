using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Characteristics.Damage.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class DeathSystem : ISystem
    {
        private Filter _filter;
        private Stash<DestroySelfRequest> _destroyRequestPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<DeadTag>().Without<DestroySelfRequest>().Build();
            _destroyRequestPool = World.GetStash<DestroySelfRequest>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                _destroyRequestPool.Add(entity);
            }
        }
        
        public void Dispose()
        {
        }
    }
}