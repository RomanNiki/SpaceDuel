using Core.Movement.Aspects;
using Core.Movement.Components;
using Core.Views.Components;
using Engine.Providers;
using Modules.Pooling.Core.Factory;
using Modules.Pooling.Engine.Pools;
using Scellecs.Morpeh;

namespace Engine.Services.AssetManagement.Pools
{
    public class EntityProviderPool : MonoPool<PoolableEntityProvider>, IFactory<SpawnRequest, World, EntityProvider>
    {
        private World _world;

        public EntityProviderPool(IFactory<PoolableEntityProvider> factory, string name, Settings settings) : base(
            factory, name, settings)
        {
        }

        public EntityProvider Create(SpawnRequest data, World world)
        {
            _world ??= world;
            var transformFactory = world.GetAspectFactory<TransformAspect>();
            var provider = Create();
            provider.Init(world, data.Entity);
            var aspectTransform = transformFactory.Get(data.Entity);
            aspectTransform.Position.Value = data.Position;
            aspectTransform.Rotation.Value = data.Rotation;
            return provider;
        }
    }
}