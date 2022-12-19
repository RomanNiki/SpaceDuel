using Leopotam.Ecs;
using Model.Components.Tags.Buffs;

namespace Extensions.Factories.Buffs
{
    public class BuffEntityFactoryFromSo : EntityFactoryFromSo
    {
        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<BuffTag>();
            return entity;
        }
    }
}