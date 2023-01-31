using System;
using Extensions;
using Extensions.Pause;
using Leopotam.Ecs;
using Model.Buffs;
using Model.Components.Events;
using Model.Components.Requests;
using Model.Extensions;
using Model.Extensions.Interfaces;
using Model.Unit.Collisions.Components.Events;
using Model.Unit.Damage.Components.Events;
using Model.Unit.Damage.Components.Requests;
using Model.Unit.Destroy.Components.Requests;
using Model.Unit.EnergySystems;
using Model.Unit.EnergySystems.Components.Events;
using Model.Unit.EnergySystems.Components.Requests;
using Model.Unit.Input.Components.Events;
using Model.Unit.Movement;
using Model.Unit.Movement.Components;
using Model.VisualEffects.Components.Events;
using Model.Weapons.Components.Events;
using UnityEngine;
using Zenject;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif

public sealed class Startup : IDisposable, ITickable, IFixedTickable, IInitializable
{
    private readonly EcsWorld _world;
    private readonly EcsSystems _systems;
    private readonly EcsSystems _fixedSystems;
    private readonly SystemRegisterHandler _systemRegister;
    private readonly PlayerRotateSystem.Settings _rotateSettings;
    private readonly PauseService _pauseService;
    private readonly VisualEffectsEntityFactories _visualEffectsEntityFactories;
    private readonly IMoveClamper _moveClamper;
    private readonly SunChargeSystem.Settings _sunChargeSettings;
    private readonly PlayersScore _playersScore;
    private readonly SunBuffEntityExecuteSystem.Settings _sunBuffSettings;
    private readonly PlayerForceSystem.Settings _forceSettings;
    [Inject]
    public Startup(EcsWorld world, SystemRegisterHandler systemRegister,
        [Inject(Optional = true)] PlayerRotateSystem.Settings rotateSettings,
        [Inject(Optional = true)] PauseService pauseService,
        [Inject(Optional = true)] MoveClamper moveClamper,
        [Inject(Optional = true)] VisualEffectsEntityFactories visualEffectsEntityFactories,
        [Inject(Optional = true)] SunChargeSystem.Settings sunChargeSettings,
        [Inject(Optional = true)] PlayersScore playersScore,
        [Inject(Optional = true)] SunBuffEntityExecuteSystem.Settings sunBuffSettings,
        [Inject(Optional = true)] PlayerForceSystem.Settings forceSettings
    )
    {
        Time.timeScale = 1f;
        _world = world;
        _systems = new EcsSystems(_world);
        _fixedSystems = new EcsSystems(_world);
        _systemRegister = systemRegister;
#if UNITY_EDITOR
        EcsWorldObserver.Create(_world);
        EcsSystemsObserver.Create(_systems);
        EcsSystemsObserver.Create(_fixedSystems);
#endif
        _visualEffectsEntityFactories = visualEffectsEntityFactories;
        _rotateSettings = rotateSettings;
        _pauseService = pauseService;
        _moveClamper = moveClamper;
        _sunChargeSettings = sunChargeSettings;
        _playersScore = playersScore;
        _sunBuffSettings = sunBuffSettings;
        _forceSettings = forceSettings;
    }

    public void Tick()
    {
        _systems?.Run();
    }

    public void FixedTick()
    {
        _fixedSystems?.Run();
    }

    public void Dispose()
    {
        _systems?.Destroy();
        _fixedSystems?.Destroy();
        _world?.Destroy();
    }

    public void Initialize()
    {
        foreach (var system in _systemRegister.RunSystems)
        {
            _systems.Add(system);
        }

        foreach (var system in _systemRegister.FixedRunSystems)
        {
            _fixedSystems.Add(system);
        }

        InjectGameData();
        AddRunOneFrames();
        AddFixedRunOneFrames();

        _systems?.Init();
        _fixedSystems?.Init();
    }

    private void AddFixedRunOneFrames()
    {
        _fixedSystems
            .OneFrame<ViewUpdateRequest>()
            .OneFrame<InputAnyKeyEvent>()
            .OneFrame<ForceRequest>()
            .OneFrame<CollisionEnterEvent>()
            .OneFrame<TriggerEnterEvent>()
            .OneFrame<ContainerComponents<CollisionEnterEvent>>()
            .OneFrame<ContainerComponents<TriggerEnterEvent>>()
            .OneFrame<DamageRequest>()
            .OneFrame<HealthChangeEvent>()
            .OneFrame<EntityDestroyRequest>()
            .OneFrame<ExplosionEvent>()
            .OneFrame<GameRestartRequest>();
    }

    private void AddRunOneFrames()
    {
        _systems
            .OneFrame<PauseEvent>()
            .OneFrame<InputAnyKeyEvent>()
            .OneFrame<InputPauseQuitEvent>()
            .OneFrame<InputRotateStartedEvent>()
            .OneFrame<InputRotateCanceledEvent>()
            .OneFrame<InputAccelerateEvent>()
            .OneFrame<InputAccelerateCanceledEvent>()
            .OneFrame<InputShootStartedEvent>()
            .OneFrame<InputShootCanceledEvent>()
            .OneFrame<ShotMadeEvent>()
            .OneFrame<DischargeRequest>()
            .OneFrame<ChargeRequest>()
            .OneFrame<EnergyChangedEvent>()
            .OneFrame<BackToMenuRequest>()
            .OneFrame<ViewCreateRequest>();
    }

    private void InjectGameData()
    {
        if (_sunChargeSettings != null)
        {
            _systems.Inject(_sunChargeSettings);
        } 
        
        if (_sunBuffSettings != null)
        {
            _systems.Inject(_sunBuffSettings);
        }

        if (_playersScore != null)
        {
            _fixedSystems.Inject(_playersScore);
        }

        if (_moveClamper != null)
        {
            _fixedSystems.Inject(_moveClamper);
        }

        if (_pauseService != null)
        {
            _systems.Inject(_pauseService);
            _fixedSystems.Inject(_pauseService);
        }

        if (_visualEffectsEntityFactories != null)
        {
            _fixedSystems.Inject(_visualEffectsEntityFactories);
        }

        if (_rotateSettings != null)
        {
            _fixedSystems.Inject(_rotateSettings);
        }  
        
        if (_forceSettings != null)
        {
            _fixedSystems.Inject(_forceSettings);
        }
    }
}