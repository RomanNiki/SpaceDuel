using Model.Extensions.EntityFactories;

namespace Model.Extensions
{
    public class VisualEffectsEntityFactories
    {
        public IEntityFactory ExplosionEntityFactory { get; }
        public IEntityFactory HitEntityFactory { get; }

        public VisualEffectsEntityFactories(IEntityFactory explosionEntityFactory, IEntityFactory hitEntityFactory)
        {
            ExplosionEntityFactory = explosionEntityFactory;
            HitEntityFactory = hitEntityFactory;
        }
    }
}