using Scellecs.Morpeh;

namespace Core.Collisions.Strategies
{
    public class CombineStrategies : IEnterTriggerStrategy
    {
        private readonly IEnterTriggerStrategy _firstStrategy;
        private readonly IEnterTriggerStrategy _secondStrategy;

        public CombineStrategies(IEnterTriggerStrategy firstStrategy, IEnterTriggerStrategy secondStrategy)
        {
            _firstStrategy = firstStrategy;
            _secondStrategy = secondStrategy;
        }
        
        public void OnEnter(World world, Entity sender, Entity target)
        {
            _firstStrategy.OnEnter(world, sender, target);
            _secondStrategy.OnEnter(world, sender, target);
        }
    }
}