using System;
using Core.Common;
using Core.Input;
using Core.Movement;
using Core.Services;
using Core.Services.Factories;
using Core.Services.Meta;

namespace Engine.Services.Factories.SystemsFactories
{
    [Serializable]
    public record FeaturesFactoryArgs(IAssets Pools, IMoveLoopService MoveLoopService, PlayersSpawnPoints SpawnPoints,
        IGame Game, IScore Score, IUIFactory UIFactory, IInput Input)
    {
        public IMoveLoopService MoveLoopService { get; } = MoveLoopService;
        public IAssets Pools { get; } = Pools;
        public IInput Input { get; } = Input;
        public PlayersSpawnPoints SpawnPoints { get; } = SpawnPoints;
        public IGame Game { get; } = Game;
        public IScore Score { get; } = Score;
        public IUIFactory UIFactory { get; } = UIFactory;
    }
}