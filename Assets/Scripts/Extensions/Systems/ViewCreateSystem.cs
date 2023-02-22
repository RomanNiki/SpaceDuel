using Leopotam.Ecs;
using Model.Components.Requests;
using UnityEngine;

namespace Extensions.Systems
{
    public abstract class ViewCreateSystem<TCreateData, TFlag> : IEcsRunSystem
        where TCreateData : struct
        where TFlag : struct
    {
        protected readonly EcsWorld World = null;
        protected readonly EcsFilter<TCreateData, TFlag> Filter = null;

        public void Run()
        {
            foreach (var i in Filter)
            {
                ref var entity = ref Filter.GetEntity(i);
                ref var createRequest = ref Filter.Get1(i);

                var transform = GetTransform(entity, createRequest);

                var provider = transform.GetProvider();
                provider.SetEntity(entity);

                entity.Del<TCreateData>();

                entity.Get<ViewUpdateRequest>();
            }
        }

        protected abstract Transform GetTransform(in EcsEntity entity, in TCreateData data);
    }
}