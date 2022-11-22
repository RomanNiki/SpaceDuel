using System;
using Components.Events;
using Components.Events.InputEvents;
using Components.Requests;
using Events.InputEvents;
using Extensions;
using Leopotam.Ecs;
using Systems;
using Zenject;

public class GameStartup : IDisposable, ITickable, IFixedTickable
{
    private readonly EcsWorld _world;
    private readonly EcsSystems _systems;
    private readonly EcsSystems _fixedSystems;

    [Inject]
    public GameStartup(EcsWorld world, [Inject(Id = SystemsEnum.Run)] EcsSystems systems,
        [Inject(Id = SystemsEnum.FixedRun)] EcsSystems fixedSystems, SystemRegisterHandler systemRegister)
    {
        _world = world;
        _systems = systems;
        _fixedSystems = fixedSystems;

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
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
            .OneFrame<InputShootCanceledEvent>();
            
        _fixedSystems.OneFrame<CollisionEvent>()
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