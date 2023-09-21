using Scellecs.Morpeh;

namespace Core.Extensions.Factories
{
    public interface IEntityFactory
    {
        Entity CreateEntity(in World world);
    }
}