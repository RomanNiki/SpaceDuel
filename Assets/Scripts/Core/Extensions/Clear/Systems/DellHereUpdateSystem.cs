using Scellecs.Morpeh;

namespace Core.Extensions.Clear.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class DellHereUpdateSystem<TComponent> : ISystem where TComponent : struct, IComponent
    {
        private Stash<TComponent> _componentPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _componentPool = World.GetStash<TComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
           _componentPool.RemoveAll();
        }

        public void Dispose()
        {
        }
    }
}