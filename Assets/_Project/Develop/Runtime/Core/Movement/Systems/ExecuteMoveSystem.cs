using _Project.Develop.Runtime.Core.Movement.Aspects;
using _Project.Develop.Runtime.Core.Views.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Movement.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class ExecuteMoveSystem : IFixedSystem
    {
        private Filter _filter;
        private AspectFactory<TransformAspect> _transformAspect;
        private Stash<ViewObject> _viewPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.Extend<TransformAspect>().With<ViewObject>().Build();
            _transformAspect = World.GetAspectFactory<TransformAspect>();
            _viewPool = World.GetStash<ViewObject>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                var transform = _transformAspect.Get(entity);
                ref var view = ref _viewPool.Get(entity);
                
                view.Value.MoveTo(transform.Position.Value);
                view.Value.RotateTo(transform.Rotation.Value);
            }
        }

        public void Dispose()
        {
        }
    }
}