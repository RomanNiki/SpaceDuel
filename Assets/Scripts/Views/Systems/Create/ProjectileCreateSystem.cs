using Extensions;
using Extensions.Systems;
using Leopotam.Ecs;
using Model.Components.Extensions.Pool;
using Model.Components.Requests;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Views.Systems.Create
{
    public abstract class ProjectileCreateSystem<TFlag> : ViewCreateSystem<ViewCreateRequest, TFlag> where TFlag : struct
    {
        protected sealed override Transform GetTransform(in EcsEntity entity, in ViewCreateRequest data)
        {
            var poolObject = GetPoolObject();
            var transform = poolObject.Transform;
            transform.position = data.StartPosition;

            var rigidbody2D = poolObject.Rigidbody2D;
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(rigidbody2D, poolObject);

            return transform;
        }

        protected abstract IPoolObject GetPoolObject();
    }
}