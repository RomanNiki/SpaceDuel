using Extensions;
using Extensions.Systems;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Extensions;
using UnityEngine;
using Views.Extensions;
using Views.Extensions.Pools;

namespace Views.Systems.Create.Effects
{
    public class VisualEffectViewCreateSystem<TFlag> : ViewCreateSystem<ViewCreateRequest, TFlag>
        where TFlag : struct
    {
        private readonly VisualEffectViewFactory _factory;

        public VisualEffectViewCreateSystem(VisualEffectViewFactory factory)
        {
            _factory = factory;
        }

        protected sealed override Transform GetTransform(in EcsEntity entity, in ViewCreateRequest data)
        {
            var explosionView = GetPoolObject();
            var transform = explosionView.Transform;
            transform.position = data.StartPosition;
            entity.Get<UnityComponent<EffectInteractor>>().Value = explosionView.EffectInteractor;
            entity.Get<ViewObjectComponent>().ViewObject =
                new ViewObjectUnity(transform, explosionView);

            return transform;
        }

        private IVisualEffectPoolObject GetPoolObject() => _factory.Create();
    }
}