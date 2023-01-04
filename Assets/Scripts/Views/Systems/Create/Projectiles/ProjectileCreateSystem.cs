using Extensions;
using Extensions.Systems;
using Leopotam.Ecs;
using Model.Components.Extensions.Interfaces.Pool;
using Model.Components.Requests;
using Model.Components.Unit.MoveComponents;
using UnityEngine;

namespace Views.Systems.Create.Projectiles
{
    public abstract class ProjectileCreateSystem<TFlag> : ViewCreateSystem<ViewCreateRequest, TFlag> where TFlag : struct
    {
        protected sealed override Transform GetTransform(in EcsEntity entity, in ViewCreateRequest data)
        {
            var poolObject = GetPoolObject();
            var transform = poolObject.Transform;
            transform.position = data.StartPosition;
            var rigidbody2D = poolObject.Rigidbody2D;
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(transform, rigidbody2D, poolObject);

            return transform;
        }

        protected abstract IPhysicsPoolObject GetPoolObject();
    }
}