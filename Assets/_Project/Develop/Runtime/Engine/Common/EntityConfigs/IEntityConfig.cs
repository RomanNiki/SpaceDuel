using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.Common.EntityConfigs
{
    public interface IEntityConfig
    {
        Entity Resolve(World world, Entity entity);
        Entity Resolve(World world);
    }
}