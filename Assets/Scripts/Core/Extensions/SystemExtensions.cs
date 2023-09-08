using Core.Extensions.Pause.Components;
using Scellecs.Morpeh;

namespace Core.Extensions
{
    public static class SystemExtensions
    {
        public static void SendMessage<T>(this World world, T component)
            where T : struct, IComponent
        {
            var pool = world.GetStash<T>();
            var entity = world.CreateEntity();
            pool.Add(entity, component);
        }

        public static IFixedSystem AddExternalPause(this IFixedSystem system)
        {
            return new FixedPauseProxySystem(system);
        }

        public static ISystem AddExternalPause(this ISystem system)
        {
            return new PauseProxySystem(system);
        }
    }

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public class PauseProxySystem : ISystem
    {
        private readonly ISystem _system;
        private Filter _pauseFilter;

        public PauseProxySystem(ISystem system)
        {
            _system = system;
        }

        public void Dispose()
        {
            _system.Dispose();
        }

        public void OnAwake()
        {
            _pauseFilter = World.Filter.With<PauseTag>().Build();
            _system.OnAwake();
        }

        public World World
        {
            get => _system.World;
            set => _system.World = value;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_pauseFilter.IsEmpty())
            {
                _system.OnUpdate(deltaTime);
            }
        }
    }
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public class FixedPauseProxySystem : PauseProxySystem, IFixedSystem
    {
        public FixedPauseProxySystem(ISystem system) : base(system)
        {
        }
    }
}