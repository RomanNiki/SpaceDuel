using Core.Movement;
using Scellecs.Morpeh;

namespace Factories.SystemsFactories
{
    public interface ISystemFactory
    {
        public SystemsGroup CreateGameSystemGroup(World world, IMoveLoopService moveLoopService);
    }
}