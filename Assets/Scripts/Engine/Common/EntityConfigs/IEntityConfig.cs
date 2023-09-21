using Scellecs.Morpeh;

namespace Engine.Common.EntityConfigs
{
    public interface IEntityConfig
    {
        Entity Resolve(World world, Entity entity);
        Entity Resolve(World world);
    }
}