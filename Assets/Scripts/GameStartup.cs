using System;
using Events.InputEvents;
using Leopotam.Ecs;
using Systems;
using Zenject;

public class GameStartup : IDisposable, ITickable
{
    private readonly EcsWorld _world;
    private readonly EcsSystems _systems;

    public GameStartup(EcsWorld world, EcsSystems systems, SystemRegisterHandler systemRegister)
    {
        _world = world;
        _systems = systems;
        
        foreach (var system in systemRegister.RunSystems)
        {
            _systems.Add(system);
        }

        _systems.OneFrame<InputAnyKeyEvent>()
            .OneFrame<InputPauseQuitEvent>()
            .OneFrame<InputRotateStartedEvent>()
            .OneFrame<InputRotateCanceledEvent>()
            .OneFrame<InputAccelerateEvent>()
            .OneFrame<InputAccelerateCanceledEvent>()
            .OneFrame<InputShootStartedEvent>()
            .OneFrame<InputShootCanceledEvent>();
        _systems.Init();
    }

    public void Dispose()
    {
        _systems.Destroy();
        _world.Destroy();
    }

    public void Tick()
    {
        _systems.Run();
    }
}