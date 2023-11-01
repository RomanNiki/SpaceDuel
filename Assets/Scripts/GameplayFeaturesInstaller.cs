using System.Linq;
using Engine.Providers;
using Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using VContainer;

public sealed class GameplayFeaturesInstaller : BaseFeaturesInstaller
{
    private IFeaturesFactory _featuresFactory;
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
            entity.Resolve(_world, _world.CreateEntity());
        }
    }

    protected override UpdateFeature[] InitializeUpdateFeatures() =>
        _featuresFactory.CreateUpdateFeatures(_featuresFactoryArgs).ToArray();

    protected override FixedUpdateFeature[] InitializeFixedUpdateFeatures() =>
        _featuresFactory.CreateFixedUpdateFeatures(_featuresFactoryArgs).ToArray();

    protected override LateUpdateFeature[] InitializeLateUpdateFeatures() =>
        _featuresFactory.CreateLateUpdateFeatures(_featuresFactoryArgs).ToArray();
}