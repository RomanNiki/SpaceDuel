using System;
using Core.Movement;
using Engine.Extensions;

namespace Engine.Factories.SystemsFactories
{
    [Serializable]
    public record FeaturesFactoryArgs(ObjectPools Pools, IMoveLoopService MoveLoopService)
    {
        public IMoveLoopService MoveLoopService { get; } = MoveLoopService;
        public ObjectPools Pools { get; } = Pools;
    }
}