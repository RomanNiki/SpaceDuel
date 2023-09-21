using System;
using Core.Common;
using Core.Movement;

namespace Engine.Factories.SystemsFactories
{
    [Serializable]
    public record FeaturesFactoryArgs(IAssets Pools, IMoveLoopService MoveLoopService)
    {
        public IMoveLoopService MoveLoopService { get; } = MoveLoopService;
        public IAssets Pools { get; } = Pools;
    }
}