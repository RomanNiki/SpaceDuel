using System;
using Core.Movement;
using Engine.Extensions;

namespace Engine.Factories.SystemsFactories
{
    [Serializable]
    public class SystemFactoryArgs
    {
        public IMoveLoopService MoveLoopService { get; }
        public ObjectPools Pools { get; }

        public SystemFactoryArgs(IMoveLoopService moveLoopService, ObjectPools pools)
        {
            MoveLoopService = moveLoopService;
            Pools = pools;
        }
    }
}