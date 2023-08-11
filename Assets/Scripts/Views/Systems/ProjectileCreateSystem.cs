using Core.Views.Components;
using Entities;
using Scellecs.Morpeh;
using Zenject;

namespace Views.Systems
{
    public sealed class ProjectileCreateSystem<TFlag> : ViewCreateSystemBase<ViewCreateRequest, TFlag>
        where TFlag : struct, IComponent
    {
        private readonly IFactory<EntityProvider> _factory;

        public ProjectileCreateSystem(IFactory<EntityProvider> factory)
        {
            _factory = factory;
        }

        protected override EntityProvider GetProvider(in Entity entity)
        {
            var provider = _factory.Create();
            return provider;
        }

        protected override void SetData(EntityProvider entityProvider, in ViewCreateRequest data)
        {
            var transform = entityProvider.transform;
            transform.position = data.Position;
            var angles = transform.eulerAngles;
            angles.z = data.Rotation;
            transform.eulerAngles = angles;
        }
    }
}