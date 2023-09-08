using Engine.Providers;
using Scellecs.Morpeh;

namespace Engine.Views.Systems.Create
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public abstract class ViewCreateSystemBase<TCreateData, TFlag> : ISystem
        where TCreateData : struct, IComponent
        where TFlag : struct, IComponent
    {
        protected Filter Filter;
        protected Stash<TCreateData> CreateDataPool;
        protected Stash<TFlag> FlagPool;
        public World World { get; set; }

        public void OnAwake()
        {
            Filter = World.Filter.With<TCreateData>().Build();
            CreateDataPool = World.GetStash<TCreateData>();
            FlagPool = World.GetStash<TFlag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in Filter)
            {
                ref var createRequest = ref CreateDataPool.Get(entity);
                if (CheckEntityTag(createRequest) == false)
                {
                    continue;
                }

                var provider = CreateView();
                SetData(provider, createRequest);
                InitProvider(provider, createRequest);
                World.RemoveEntity(entity);
            }
        }

        protected abstract void InitProvider(EntityProvider provider, TCreateData createData);

        protected abstract bool CheckEntityTag(TCreateData entity);

        protected abstract EntityProvider CreateView();

        protected abstract void SetData(EntityProvider transform, in TCreateData data);

        public void Dispose()
        {
        }
    }
}