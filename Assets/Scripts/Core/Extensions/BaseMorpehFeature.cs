using System;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;

namespace Core.Extensions
{
    public abstract class BaseMorpehFeature : IDisposable
    {
        protected SystemsGroup FeatureSystemGroup;
        protected World World;

        protected void AddSystem(ISystem system) => FeatureSystemGroup.AddSystem(system);

        protected void AddSystem(IInitializer system) => FeatureSystemGroup.AddInitializer(system);
        

        public async UniTask InitializeFeatureAsync(World world, int order, bool enabled = true)
        {
            FeatureSystemGroup = world.CreateSystemsGroup();
            World = world;
            await InitializeSystems();
            World.AddSystemsGroup(order, FeatureSystemGroup);
        }

        protected abstract UniTask InitializeSystems();

        public void Dispose()
        {
            OnDispose();
            World.RemoveSystemsGroup(FeatureSystemGroup);
        }

        protected virtual void OnDispose()
        {
        }
    }
}