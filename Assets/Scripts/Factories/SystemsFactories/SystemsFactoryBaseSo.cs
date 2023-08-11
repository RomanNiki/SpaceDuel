using Core.Movement;
using Scellecs.Morpeh;
using UnityEngine;

namespace Factories.SystemsFactories
{
    public abstract class SystemsFactoryBaseSo : ScriptableObject, ISystemFactory
    {
        public abstract SystemsGroup CreateGameSystemGroup(World world, IMoveLoopService moveLoopService);
    }
}