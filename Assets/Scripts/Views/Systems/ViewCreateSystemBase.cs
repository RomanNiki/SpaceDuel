using Core.Views.Components;
using Entities;
using Scellecs.Morpeh;

namespace Views.Systems
{
    public abstract class ViewCreateSystemBase<TCreateData, TFlag> : ISystem
        where TCreateData : struct, IComponent
        where TFlag : struct, IComponent
    {
        protected Filter Filter;
        protected Stash<TCreateData> CreateDataPool;
        protected Stash<ViewUpdateRequest> ViewUpdateRequestPool;
        public World World { get; set; }
        
        public void OnAwake()
        {
            Filter = World.Filter.With<TCreateData>().With<TFlag>();
            CreateDataPool = World.GetStash<TCreateData>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in Filter)
            {
                ref var createRequest = ref CreateDataPool.Get(entity);
                var provider = GetProvider(entity, createRequest);
                provider.Init(World, entity);
                CreateDataPool.Remove(entity);
                ViewUpdateRequestPool.Add(entity);
            }
        }

        protected abstract EntityProvider GetProvider(in Entity entity, in TCreateData data);

        public void Dispose()
        {
        }
    }
}