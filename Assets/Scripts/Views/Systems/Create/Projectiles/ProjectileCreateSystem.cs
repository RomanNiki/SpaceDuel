using Extensions;
using Extensions.Systems;
using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Extensions;
using UnityEngine;
using Views.Extensions.Pools;

namespace Views.Systems.Create.Projectiles
{
    public class ProjectileCreateSystem<TFlag> : ViewCreateSystem<ViewCreateRequest, TFlag> where TFlag : struct
    {
        private readonly ProjectileViewFactory _factory;

        public ProjectileCreateSystem(ProjectileViewFactory factory)
        {
            _factory = factory;
        }

        protected sealed override Transform GetTransform(in EcsEntity entity, in ViewCreateRequest data)
        {
            var poolObject = GetPoolObject();
            var transform = poolObject.Transform;
            transform.position = data.StartPosition;
            var rigidbody2D = poolObject.Rigidbody2D;
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(transform, rigidbody2D, poolObject);

            return transform;
        }

        private IPhysicsPoolObject GetPoolObject() => _factory.Create();
    }
}