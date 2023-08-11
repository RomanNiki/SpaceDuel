using Scellecs.Morpeh;

namespace Core.Extensions
{
    public interface ISystemsFactory
    {
        public SystemsGroup CreateSystems(World world);
        public SystemsGroup CreateFixedSystems(World world);
    }
}