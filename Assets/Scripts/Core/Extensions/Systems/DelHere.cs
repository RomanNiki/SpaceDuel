using Scellecs.Morpeh;

namespace Core.Extensions.Systems
{
    public class DelHere<T> : ICleanupSystem where T : struct, IComponent
    {
        private Filter _filter;
        private Stash<T> _pool;
        public World World { get; set; }

        public void Dispose()
        {
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<T>();
            _pool = World.GetStash<T>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                _pool.Remove(entity);
            }
        }
    }
}