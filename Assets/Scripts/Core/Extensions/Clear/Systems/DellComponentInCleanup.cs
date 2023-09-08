using Scellecs.Morpeh;

namespace Core.Extensions.Clear.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public sealed class DellComponentInCleanup<T> : ICleanupSystem where T : struct, IComponent
    {
        private Stash<T> _pool;
        public World World { get; set; }

        public void Dispose()
        {
        }

        public void OnAwake()
        {
            _pool = World.GetStash<T>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            _pool.RemoveAll();
        }
    }
}