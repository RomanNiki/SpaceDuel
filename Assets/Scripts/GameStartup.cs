using System;
using Components.Events.InputEvents;
using Events.InputEvents;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using Model.Components.Events;
using Model.Components.Events.InputEvents;
using Model.Components.Extensions;
using Model.Components.Requests;
using Model.Systems;
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
        Time.timeScale = 1f;
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
        
        _systems.Init();
        _fixedSystems.Init();
    }

    public void Tick()
    {
        _systems.Run();
    }

    public void FixedTick()
    {
        _fixedSystems.Run();
    }

    public void Dispose()
    {
        if (_debugObjects != null)
        {
            foreach (var i in _debugObjects)
            {
                if (i)
                {
                    Object.Destroy(i);
                }
            }
        }

        _systems?.Destroy();

        _fixedSystems?.Destroy();
        _world?.Destroy();
    }
}