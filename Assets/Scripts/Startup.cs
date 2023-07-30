using System;
using Extensions;
using Installers;
using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Extensions;
using Model.Extensions.Interfaces;
using Model.Extensions.Pause;
using Model.Unit.Collisions.Components.Events;
using Model.Unit.Damage.Components.Events;
using Model.Unit.Damage.Components.Requests;
using Model.Unit.Destroy.Components.Requests;
using Model.Unit.EnergySystems.Components.Events;
using Model.Unit.EnergySystems.Components.Requests;
using Model.Unit.Input.Components.Events;
using Model.Unit.Movement.Components;
using Model.VisualEffects.Components.Events;
using Model.Weapons.Components.Events;
using UnityEngine;
using Zenject;

public sealed class Startup : IDisposable, ITickable, IFixedTickable, IInitializable
{
    private readonly EcsWorld _world;
    private readonly EcsSystems _systems;
    private readonly EcsSystems _fixedSystems;
    private readonly SystemRegisterHandler _systemRegister;
    private readonly IPauseService _pauseService;
    private readonly IMoveClamper _moveClamper;
    private readonly PlayersScore _playersScore;
    private readonly GameSettingsInstaller.GameSettings _gameSettings;
    private readonly GameSettingsInstaller.PlayerSettings _playerSettings;

    [Inject]
    public Startup(EcsWorld world, SystemRegisterHandler systemRegister,
        GameSettingsInstaller.GameSettings gameSettings, GameSettingsInstaller.PlayerSettings playerSettings,
        IPauseService pauseService,
        IMoveClamper moveClamper,
        PlayersScore playersScore
    )
    {
        Time.timeScale = 1f;
        _world = world;
        _systems = new EcsSystems(_world);
        _fixedSystems = new EcsSystems(_world);
        _systemRegister = systemRegister;
        _playerSettings = playerSettings;
        _gameSettings = gameSettings;
        _pauseService = pauseService;
        _moveClamper = moveClamper;
        _playersScore = playersScore;
        /*#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedSystems);
#endif*/
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
            .OneFrame<ExplosionEvent>();
    }

    private void AddRunOneFrames()
    {
        _systems
            .OneFrame<InputAnyKeyEvent>()
            .OneFrame<InputPauseQuitEvent>()
            .OneFrame<InputRotateStartedEvent>()
            .OneFrame<InputRotateCanceledEvent>()
            .OneFrame<InputAccelerateStartedEvent>()
            .OneFrame<InputAccelerateCanceledEvent>()
            .OneFrame<InputShootStartedEvent>()
            .OneFrame<InputShootCanceledEvent>()
            .OneFrame<ShotMadeEvent>()
            .OneFrame<DischargeRequest>()
            .OneFrame<ChargeRequest>()
            .OneFrame<EnergyChangedEvent>()
            .OneFrame<ViewCreateRequest>();
    }

    private void InjectGameData()
    {
        if (_playerSettings != null)
        {
            _systems.Inject(_playerSettings.SolarSettings);
        }

        if (_gameSettings != null)
        {
            _systems.Inject(_gameSettings.BuffSettings);
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
    }
}