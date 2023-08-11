using Core.Collisions.Components;
using Scellecs.Morpeh;

namespace Core.Collisions.Systems
{
    public abstract class CollisionSystemBase<TComponentTag> : IFixedSystem
        where TComponentTag : struct, IComponent
    {
        private Filter _filter;
        private Stash<ComponentQueue<CollisionEvent>> _collisionsPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<TComponentTag>().With<ComponentQueue<CollisionEvent>>();
            _collisionsPool = World.GetStash<ComponentQueue<CollisionEvent>>();
            OnInit();
        }

        protected abstract void OnInit();

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var entityCollisionQueue = ref _collisionsPool.Get(entity);
                for (var i = 0; i < entityCollisionQueue.Values.Count; i++)
                {
                    var collisionEvent = entityCollisionQueue.Values.Dequeue();
                    if (TryCollide(collisionEvent, entity))
                    {
                        entityCollisionQueue.Values.Enqueue(collisionEvent);
                    }
                }
            }
        }

        protected abstract bool TryCollide(CollisionEvent collisionEvent, in Entity entity);

        public void Dispose()
        {
        }
    }
}