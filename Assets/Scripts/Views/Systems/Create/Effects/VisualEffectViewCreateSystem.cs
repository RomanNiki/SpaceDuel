using Extensions;
using Extensions.Systems;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Extensions.Pool;
using Model.Components.Requests;
using Model.Components.Unit.MoveComponents;
using UnityEngine;
using UnityEngine.VFX;

namespace Views.Systems.Create.Effects
{
    public abstract class VisualEffectViewCreateSystem<TFlag> : ViewCreateSystem<ViewCreateRequest, TFlag>
        where TFlag : struct
    {
        protected sealed override Transform GetTransform(in EcsEntity entity, in ViewCreateRequest data)
        {
            var explosionView = GetPoolObject();
            var transform = explosionView.Transform;
            transform.position = data.StartPosition;
            entity.Get<UnityComponent<VisualEffect>>().Value = explosionView.VisualEffect;
            entity.Get<ViewObjectComponent>().ViewObject =
                new ViewObjectUnity(transform, explosionView);

            return transform;
        }

        protected abstract IVisualEffectPoolObject GetPoolObject();
    }
}