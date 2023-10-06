using Scellecs.Morpeh;

namespace Core.Services.Factories
{
    public interface IEntityFactory
    {
        Entity CreateEntity(in World world);
    }
}