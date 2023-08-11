using Core.Views.Components;
using Entities;
using Scellecs.Morpeh;
using Zenject;

namespace Views.Systems
{
    public class ProjectileCreateSystem<TFlag, TValue> : ViewCreateSystemBase<ViewCreateRequest, TFlag>
        where TFlag : struct, IComponent

    {
        private readonly IFactory<EntityProvider> _factory;

        public ProjectileCreateSystem(IFactory<EntityProvider> memoryPool)
        {
            _factory = memoryPool;
        }

        protected override EntityProvider GetProvider(in Entity entity, in ViewCreateRequest data)
        {
            var provider = _factory.Create();
            var providerTransform = provider.transform;
            var angles = providerTransform.eulerAngles;
            angles.z = data.Rotation;
            providerTransform.eulerAngles = angles;
            return provider;
        }
    }
}