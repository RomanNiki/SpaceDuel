using Core.Views.Components;
using Engine.Providers;
using Modules.Pooling.Factory;
using Scellecs.Morpeh;

namespace Engine.Views.Systems.Create
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    
    public sealed class ProjectileCreateSystem<TFlag> : ViewCreateSystemBase<ViewCreateRequest, TFlag>
        where TFlag : struct, IComponent
    {
        private readonly IFactory<EntityProvider> _factory;

        public ProjectileCreateSystem(IFactory<EntityProvider> factory)
        {
            _factory = factory;
        }

        protected override void InitProvider(EntityProvider provider, ViewCreateRequest createRequest)
        {
            provider.Init(World, createRequest.Entity);
        }

        protected override bool CheckEntityTag(ViewCreateRequest createRequest)
        {
            return FlagPool.Has(createRequest.Entity);
        }

        protected override EntityProvider CreateView()
        {
            return _factory.Create();
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