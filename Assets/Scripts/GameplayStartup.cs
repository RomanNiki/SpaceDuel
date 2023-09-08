using System;
using System.Linq;
using Core.Extensions;
using Core.Extensions.Views;
using Core.Movement;
using Cysharp.Threading.Tasks;
using Engine.Factories.SystemsFactories;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;
using EntityProvider = Engine.Providers.EntityProvider;

public sealed class GameplayStartup : BaseInstaller
{
    [SerializeReference]
    private BaseMorpehFeature[] _updateFeatures = Array.Empty<BaseMorpehFeature>();
    [SerializeReference]
    private BaseMorpehFeature[] _fixedFeatures = Array.Empty<BaseMorpehFeature>();
    [SerializeReference]
    private BaseMorpehFeature[] _lateFeatures = Array.Empty<BaseMorpehFeature>();
    private ISystemFactory _systemsFactory;
    private IMoveLoopService _moveLoopService;
    private SystemFactoryArgs _systemFactoryArgs;

    [Inject]
    public void Constructor(World world, ISystemFactory systemsFactory, SystemFactoryArgs systemFactoryArgs)
    {
        _systemFactoryArgs = systemFactoryArgs;
        World = world;
        World.UpdateByUnity = true;
        _systemsFactory = systemsFactory;
        
        CreateFeatures();
    }

    protected override async void OnEnable()
    {
        await EnableFeatures();
    }

    protected override void OnDisable()
    {
        DisableFeatures();
    }

    private async UniTask EnableFeatures()
    {
        var order = 0;
        for (var i = 0; i < _updateFeatures.Length; i++, order++)
            await World.AddFeatureAsync(order, _updateFeatures[i]);
        for (var i = 0; i < _fixedFeatures.Length; i++, order++)
            await World.AddFeatureAsync(order, _fixedFeatures[i]);
        for (var i = 0; i < _lateFeatures.Length; i++, order++)
            await World.AddFeatureAsync(order, _lateFeatures[i]);
    }

    private void DisableFeatures()
    {
        for (var i = 0; i < _updateFeatures.Length; i++)
            World.RemoveFeature(_updateFeatures[i]);
        for (var i = 0; i < _fixedFeatures.Length; i++)
            World.RemoveFeature(_fixedFeatures[i]);
        for (var i = 0; i < _lateFeatures.Length; i++)
            World.RemoveFeature(_lateFeatures[i]);
    }

    private void Start()
    {
        foreach (var entity in FindObjectsOfType<EntityProvider>())
        {
            entity.Init(World);
        }
    }

    private void Update()
    {
        World?.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        World?.FixedUpdate(Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        World?.LateUpdate(Time.deltaTime);
        World?.CleanupUpdate(Time.deltaTime);
    }

    private void OnDestroy()
    {
        World?.Dispose();
    }

    private void CreateFeatures()
    {
        _updateFeatures = _systemsFactory.CreateUpdateFeatures(_systemFactoryArgs).ToArray();
        _fixedFeatures = _systemsFactory.CreateFixedUpdateFeatures(_systemFactoryArgs).ToArray();
        _lateFeatures = _systemsFactory.CreateLateUpdateFeatures(_systemFactoryArgs).ToArray();
    }
}