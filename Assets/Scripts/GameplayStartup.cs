using System;
using System.Linq;
using Core.Extensions;
using Core.Extensions.Views;
using Core.Movement;
using Cysharp.Threading.Tasks;
using Engine.Factories.SystemsFactories;
using Scellecs.Morpeh;
using TriInspector;
using Zenject;
using EntityProvider = Engine.Providers.EntityProvider;

public sealed class GameplayStartup : BaseInstaller
{
    private IFeaturesFactory _featuresFactory;
    private IMoveLoopService _moveLoopService;
    private FeaturesFactoryArgs _featuresFactoryArgs;
    
    [Inject]
    public void Constructor(World world, IFeaturesFactory featuresFactory, FeaturesFactoryArgs featuresFactoryArgs)
    {
        World = world;
        World.UpdateByUnity = true;
        _featuresFactoryArgs = featuresFactoryArgs;
        _featuresFactory = featuresFactory;
        CreateFeatures();
    }

    protected override async void OnEnable() => await EnableFeatures();

    protected override void OnDisable() => DisableFeatures();

    private async UniTask EnableFeatures()
    {
        for (var i = 0; i < _activeFeatures.Length; i++)
            await World.AddFeatureAsync(i, _activeFeatures[i]);
    }

    private void DisableFeatures()
    {
        foreach (var t in _activeFeatures)
            World.RemoveFeature(t);
    }

    private void Start()
    {
        foreach (var entity in FindObjectsOfType<EntityProvider>())
        {
            entity.Init(World);
        }
    }

    private void OnDestroy() => World?.Dispose();

    private void CreateFeatures() => _activeFeatures = _featuresFactory.Create(_featuresFactoryArgs).ToArray();
    
    #if DEBUG
    [PropertySpace]
    [ShowInInspector]
    [PropertyOrder(-1)]
    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    #endif
    private BaseMorpehFeature[] _activeFeatures = Array.Empty<BaseMorpehFeature>();
}