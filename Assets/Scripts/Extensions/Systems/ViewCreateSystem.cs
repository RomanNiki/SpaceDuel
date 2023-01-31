using Leopotam.Ecs;
using Model.Components.Requests;
using UnityEngine;

namespace Extensions.Systems
{
    public abstract class ViewCreateSystem<TCreateData, TFlag> : IEcsRunSystem
        where TCreateData : struct
        where TFlag : struct
    {
        protected readonly EcsWorld _world = null;
        protected EcsFilter<TCreateData, TFlag> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var createRequest = ref _filter.Get1(i);

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