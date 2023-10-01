using System.Linq;
using Core.Movement;
using Engine.Factories.SystemsFactories;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using Zenject;
using EntityProvider = Engine.Providers.EntityProvider;

public sealed class GameplayStartup : BaseFeaturesInstaller
{
    private IFeaturesFactory _featuresFactory;
    private IMoveLoopService _moveLoopService;
    private FeaturesFactoryArgs _featuresFactoryArgs;
    private World _world;

    [Inject]
    public void Constructor(IFeaturesFactory featuresFactory, FeaturesFactoryArgs featuresFactoryArgs)
    {
        _featuresFactoryArgs = featuresFactoryArgs;
        _featuresFactory = featuresFactory;
    }
    
    protected override void InitializeShared()
    {
        _world = World.Default;
        foreach (var entity in FindObjectsOfType<EntityProvider>())
        {
            entity.Init(_world);
        }
    }

    protected override UpdateFeature[] InitializeUpdateFeatures() =>
        _featuresFactory.CreateUpdateFeatures(_featuresFactoryArgs).ToArray();

    protected override FixedUpdateFeature[] InitializeFixedUpdateFeatures() =>
        _featuresFactory.CreateFixedUpdateFeatures(_featuresFactoryArgs).ToArray();

    protected override LateUpdateFeature[] InitializeLateUpdateFeatures() =>
        _featuresFactory.CreateLateUpdateFeatures(_featuresFactoryArgs).ToArray();
}