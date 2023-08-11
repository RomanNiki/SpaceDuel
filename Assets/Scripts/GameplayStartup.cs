using Core.Movement;
using Entities;
using Factories.SystemsFactories;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

public sealed class GameplayStartup : MonoBehaviour
{
    [SerializeField] private SystemsFactoryBaseSo _systemsFactory;
    private IMoveLoopService _moveLoopService;
    private World _world;

    [Inject]
    public void Constructor(World world, IMoveLoopService moveLoopService)
    {
        _world = world;
        _moveLoopService = moveLoopService;
    }

    private void Start()
    {
        var gameSystemGroup = _systemsFactory.CreateGameSystemGroup(_world, _moveLoopService);
        _world.AddSystemsGroup(order: 0, gameSystemGroup);

        foreach (var entity in FindObjectsOfType<EntityProvider>())
        {
            entity.Init(_world);
        }
    }

    private void Update()
    {
        _world?.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _world?.FixedUpdate(Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        _world?.LateUpdate(Time.deltaTime);
        _world?.CleanupUpdate(Time.deltaTime);
    }

    private void OnDestroy()
    {
        _world?.Dispose();
    }
}