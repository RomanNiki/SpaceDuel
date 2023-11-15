using System;
using _Project.Develop.Runtime.Core.Input;
using _Project.Develop.Runtime.Core.Movement;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Factories;
using _Project.Develop.Runtime.Core.Services.Meta;

namespace _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories
{
    [Serializable]
    public record FeaturesFactoryArgs(IAssets Assets, IMoveLoopService MoveLoopService,
        IGame Game, IScore Score, IUIFactory UIFactory, IInput Input)
    {
        public IMoveLoopService MoveLoopService { get; } = MoveLoopService;
        public IAssets Assets { get; } = Assets;
        public IInput Input { get; } = Input;
        public IGame Game { get; } = Game;
        public IScore Score { get; } = Score;
        public IUIFactory UIFactory { get; } = UIFactory;
    }
}