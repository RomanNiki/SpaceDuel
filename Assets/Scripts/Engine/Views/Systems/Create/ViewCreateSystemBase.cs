using Core.Extensions;
using Core.Views.Components;
using Engine.Providers;
using Scellecs.Morpeh;
using UnityEngine;

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
        protected Stash<TFlag> TagPool;
        public World World { get; set; }

        public void OnAwake()
        {
            Filter = World.Filter.With<TCreateData>().Build();
            CreateDataPool = World.GetStash<TCreateData>();
            TagPool = World.GetStash<TFlag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in Filter)
            {
                ref var createRequest = ref CreateDataPool.Get(entity);
                var viewEntity = GetEntity(createRequest);

                if (CheckEntityTag(viewEntity) == false)
                    continue;

                var provider = CreateView(createRequest);
                provider.Init(World, viewEntity);
                SetData(provider.transform, createRequest);

                World.SendMessage(new ViewCreatedEvent { Entity = viewEntity });
                World.RemoveEntity(entity);
            }
        }

        private bool CheckEntityTag(Entity createdEntity) => TagPool.Has(createdEntity);

        protected abstract Entity GetEntity(TCreateData createRequest);

        protected abstract EntityProvider CreateView(TCreateData createData);

        protected abstract void SetData(Transform transform, in TCreateData data);

        public void Dispose()
        {
        }
    }
}