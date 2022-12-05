using Extensions;
using Extensions.Systems;
using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Components.Tags;
using Model.Components.Unit.MoveComponents;
using UnityEngine;
using Zenject;

namespace Views.Systems.Create
{
    public class ExplosionViewCreateSystem : ViewCreateSystem<ViewCreateRequest, ExplosionTag>
    {
        [Inject] private ExplosionView.Factory _factory;
        
        protected override Transform GetTransform(in EcsEntity entity, in ViewCreateRequest data)
        {
            var explosionView = _factory.Create();
            var transform = explosionView.Transform;
            transform.position = data.StartPosition;
            entity.Get<ViewObjectComponent>().ViewObject =
                new TransformViewObjectUnity(transform, explosionView);
          
            return transform;
        }
    }
}