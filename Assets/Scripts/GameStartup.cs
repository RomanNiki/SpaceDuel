using System;
using Components.Events.InputEvents;
using Events.InputEvents;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using Model.Components.Events;
using Model.Components.Events.InputEvents;
using Model.Components.Extensions;
using Model.Components.Requests;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class GameStartup : IDisposable, ITickable, IFixedTickable
{
    private readonly EcsWorld _world;
    private readonly EcsSystems _systems;
    private readonly EcsSystems _fixedSystems;
#if UNITY_EDITOR
    private readonly GameObject[] _debugObjects;
#endif
    [Inject]
    public GameStartup(EcsWorld world, SystemRegisterHandler systemRegister)
    {
        _world = world;
        _systems = new EcsSystems(_world);
        _fixedSystems = new EcsSystems(_world);

#if UNITY_EDITOR
        _debugObjects = new[]
        {
            EcsWorldObserver.Create(_world),
            EcsSystemsObserver.Create(_systems),
            EcsSystemsObserver.Create(_fixedSystems)
        };
#endif
        foreach (var system in systemRegister.RunSystems)
        {
            _systems.Add(system);
        }

        foreach (var system in systemRegister.FixedRunSystems)
        {
            _fixedSystems.Add(system);
        }

        _systems
            .OneFrame<ViewUpdateRequest>()
            .OneFrame<InputAnyKeyEvent>()
            .OneFrame<InputPauseQuitEvent>()
            .OneFrame<InputRotateStartedEvent>()
            .OneFrame<InputRotateCanceledEvent>()
            .OneFrame<InputAccelerateEvent>()
            .OneFrame<InputAccelerateCanceledEvent>()
            .OneFrame<InputShootStartedEvent>()
            .OneFrame<InputShootCanceledEvent>()
            .OneFrame<ForceRequest>()
            .OneFrame<ShotMadeEvent>() 
            .OneFrame<DamageRequest>()
            .OneFrame<DischargeRequest>()
            .OneFrame<ChargeRequest>()
            .OneFrame<EnergyEndedEvent>()
            .OneFrame<ViewCreateRequest>();

        _fixedSystems.OneFrame<ForceRequest>()
            .OneFrame<EnergyEndedEvent>()
            .OneFrame<CollisionEnterEvent>()
            .OneFrame<TriggerEnterEvent>()
            .OneFrame<HealthChangeEvent>()
            .OneFrame<DamageRequest>();
        _systems.Init();
        _fixedSystems.Init();
    }

    public void Dispose()
    {
        _systems.Destroy();
        _fixedSystems.Destroy();
        _world.Destroy();
    }

    public void Tick()
    {
        _systems.Run();
    }

    public void FixedTick()
    {
        _fixedSystems.Run();
    }
}