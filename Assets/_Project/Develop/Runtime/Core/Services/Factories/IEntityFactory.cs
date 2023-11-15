using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Services.Factories
{
    public interface IEntityFactory
    {
        Entity CreateEntity(in World world);
    }
}