using Core.Extensions;
using Core.Views.Components;
using Scellecs.Morpeh;

namespace Core.Views.Systems.Create
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public abstract class SpawnSystemBase<TCreateData> : ISystem
        where TCreateData : struct, IComponent
    {
        protected Filter Filter;
        protected Stash<TCreateData> CreateDataPool;
        public World World { get; set; }

        public void OnAwake()
        {
            Filter = World.Filter.With<TCreateData>().Build();
            CreateDataPool = World.GetStash<TCreateData>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in Filter)
            {
                ref var createRequest = ref CreateDataPool.Get(entity);
                var viewEntity = CreateView(createRequest);
                World.SendMessage(new SpawnedEvent { Entity = viewEntity });
                World.RemoveEntity(entity);
            }
        }

        protected abstract Entity CreateView(TCreateData createData);

        public void Dispose()
        {
        }
    }
}