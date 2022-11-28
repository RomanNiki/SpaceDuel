using Leopotam.Ecs;

namespace Model.Components.Extensions.EntityFactories
{
    public interface IEntityFactory
    {
        EcsEntity CreateEntity(EcsWorld world);
    }
}