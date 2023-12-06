using System;
using _Project.Develop.Runtime.Core.Input;
using _Project.Develop.Runtime.Core.Movement;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Factories;
using _Project.Develop.Runtime.Core.Services.Meta;
using _Project.Develop.Runtime.Core.Services.Pause;
using _Project.Develop.Runtime.Core.Services.Random;

namespace _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories
{
    [Serializable]
    public record FeaturesArgs(IAssets Assets, IMoveLoopService MoveLoopService,
        IGame Game, IScore Score, IUIFactory UIFactory, IInput Input, IRandom Random, IPauseService PauseService)
    {
        public IMoveLoopService MoveLoopService { get; } = MoveLoopService;
        public IAssets Assets { get; } = Assets;
        public IInput Input { get; } = Input;
        public IGame Game { get; } = Game;
        public IScore Score { get; } = Score;
        public IUIFactory UIFactory { get; } = UIFactory;
        public IRandom Random { get; } = Random;
        public IPauseService PauseService { get; set; } = PauseService;
    }
}