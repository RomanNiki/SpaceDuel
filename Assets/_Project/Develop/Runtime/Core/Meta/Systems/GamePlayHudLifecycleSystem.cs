using _Project.Develop.Runtime.Core.Services.Factories;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Meta.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class GamePlayHudLifecycleSystem : ISystem
    {
        private readonly IUIFactory _uiFactory;

        public GamePlayHudLifecycleSystem(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public World World { get; set; }

        public void OnAwake()
        {
            _uiFactory.OpenGameplayHud();
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
            _uiFactory.CloseGameplayHud();
        }
    }
}