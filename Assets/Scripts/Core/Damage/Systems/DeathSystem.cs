using Core.Damage.Components;
using Scellecs.Morpeh;

namespace Core.Damage.Systems
{
    public sealed class DeathSystem : ISystem
    {
        private Filter _filter;
        private Stash<DestroyRequest> _destroyRequestPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<DeadTag>();
            _destroyRequestPool = World.GetStash<DestroyRequest>();
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