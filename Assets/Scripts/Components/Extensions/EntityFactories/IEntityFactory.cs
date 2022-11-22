using Leopotam.Ecs;

namespace Components.Extensions.EntityFactories
{
    public interface IEntityFactory
    {
        EcsEntity CreateEntity(EcsWorld world);
    }
}