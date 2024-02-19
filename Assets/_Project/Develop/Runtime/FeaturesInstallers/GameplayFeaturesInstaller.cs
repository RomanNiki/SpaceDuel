using System.Linq;
using _Project.Develop.Runtime.Engine.Providers;
using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using VContainer;

namespace _Project.Develop.Runtime.FeaturesInstallers
{
    public sealed class GameplayFeaturesInstaller : BaseFeaturesInstaller
    {
        private IFeaturesFactory _featuresFactory;
        private IObjectResolver _container;
        

        [Inject]
        public void Init(IFeaturesFactory featuresFactory, IObjectResolver container)
        {
            _container = container;
            _featuresFactory = featuresFactory;
        }
    
        protected override void InitializeShared()
        {
            foreach (var entity in FindObjectsOfType<EntityProvider>())
            {
                entity.Resolve(defaultWorld, defaultWorld.CreateEntity());
            }
        }

        protected override UpdateFeature[] InitializeUpdateFeatures() =>
            _featuresFactory.CreateUpdateFeatures(_container).ToArray();

        protected override FixedUpdateFeature[] InitializeFixedUpdateFeatures() =>
            _featuresFactory.CreateFixedUpdateFeatures(_container).ToArray();

        protected override LateUpdateFeature[] InitializeLateUpdateFeatures() =>
            _featuresFactory.CreateLateUpdateFeatures(_container).ToArray();
    }
}