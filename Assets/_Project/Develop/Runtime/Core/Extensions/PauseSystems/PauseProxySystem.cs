using _Project.Develop.Runtime.Core.Services.Pause.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Extensions.PauseSystems
{
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

        public void Dispose() => _system.Dispose();

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
}