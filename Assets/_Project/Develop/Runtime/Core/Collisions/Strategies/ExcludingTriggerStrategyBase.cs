using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Collisions.Strategies
{
    public abstract class ExcludingTriggerStrategyBase : IEnterTriggerStrategy
    {
        private readonly IEnterTriggerStrategy _enterTriggerStrategy;

        public ExcludingTriggerStrategyBase(IEnterTriggerStrategy enterTriggerStrategy)
        {
            _enterTriggerStrategy = enterTriggerStrategy;
        }
        
        public void OnEnter(World world, Entity sender, Entity target)
        {
            if (IsExcluded(world, sender, target))
            {
               return;
            }
            _enterTriggerStrategy.OnEnter(world, sender, target);
        }

        protected abstract bool IsExcluded(World world, Entity sender, Entity target);
    }
}