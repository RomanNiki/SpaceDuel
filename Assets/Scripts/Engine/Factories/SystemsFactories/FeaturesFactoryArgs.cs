using System;
using Core.Common;
using Core.Movement;
using Core.Services;

namespace Engine.Factories.SystemsFactories
{
    [Serializable]
    public record FeaturesFactoryArgs(IAssets Pools, IMoveLoopService MoveLoopService, PlayersSpawnPoints SpawnPoints)
    {
        public IMoveLoopService MoveLoopService { get; } = MoveLoopService;
        public IAssets Pools { get; } = Pools;
        public PlayersSpawnPoints SpawnPoints { get; } = SpawnPoints;
    }
}