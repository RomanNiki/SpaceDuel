using System;
using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Requests;
using Model.Extensions;
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
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif

public sealed class Startup : IDisposable, ITickable, IFixedTickable, IInitializable
{
    private readonly EcsWorld _world;
    private readonly EcsSystems _systems;
    private readonly EcsSystems _fixedSystems;
    private readonly SystemRegisterHandler _systemRegister;

    [Inject]
    public Startup(EcsWorld world, SystemRegisterHandler systemRegister)
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
        
        _systems?.Init();
        _fixedSystems?.Init();
    }
}