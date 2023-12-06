using System.Linq;
using _Project.Develop.Runtime.Engine.Providers;
using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using VContainer;

namespace _Project.Develop.Runtime
{
    public sealed class GameplayFeaturesInstaller : BaseFeaturesInstaller
    {
        private IFeaturesFactory _featuresFactory;
        private FeaturesArgs _featuresArgs;
        private World _world;

        [Inject]
        public void Init(IFeaturesFactory featuresFactory, FeaturesArgs featuresArgs)
        {
            _featuresArgs = featuresArgs;
            _featuresFactory = featuresFactory;
        }
    
        protected override void InitializeShared()
        {
            _world = World.Default;
            foreach (var entity in FindObjectsOfType<EntityProvider>())
            {
                entity.Resolve(_world, _world.CreateEntity());
            }
        }

        protected override UpdateFeature[] InitializeUpdateFeatures() =>
            _featuresFactory.CreateUpdateFeatures(_featuresArgs).ToArray();

        protected override FixedUpdateFeature[] InitializeFixedUpdateFeatures() =>
            _featuresFactory.CreateFixedUpdateFeatures(_featuresArgs).ToArray();

        protected override LateUpdateFeature[] InitializeLateUpdateFeatures() =>
            _featuresFactory.CreateLateUpdateFeatures(_featuresArgs).ToArray();
    }
}