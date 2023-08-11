using Core.Damage.Components;
using Core.Extensions.Components;
using Scellecs.Morpeh;

namespace Core.Damage.Systems
{
    public sealed class DestroySystem : ISystem
    {
        private Filter _filter;
        private Stash<ViewObject> _viewPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<DestroyRequest>();
            _viewPool = World.GetStash<ViewObject>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                if (_viewPool.Has(entity))
                {
                    _viewPool.Get(entity).Value.Dispose();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}