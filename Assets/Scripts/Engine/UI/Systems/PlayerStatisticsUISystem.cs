using UnityEngine;

namespace Engine.UI.Systems
{
    using Scellecs.Morpeh;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class PlayerStatisticsUISystem : ISystem
    {
        public World World { get; set; }
        private Camera _camera;

        public void OnAwake()
        {
            _camera = Camera.main;
        }

        public void OnUpdate(float deltaTime)
        { 
        }

        public void Dispose()
        {
        }
    }
}