using Core.Timers.Components;
using Scellecs.Morpeh;

namespace Core.Collisions.Strategies
{
    public class InvisibleStrategy : ExcludingTriggerStrategyBase
    {
        public InvisibleStrategy(IEnterTriggerStrategy enterTriggerStrategy) : base(enterTriggerStrategy)
        {
        }

        protected override bool IsExcluded(World world, Entity sender, Entity target)
        {
            var invisibleTimers = world.GetStash<Timer<InvisibleTimer>>();
            return invisibleTimers.Has(sender);
        }
    }
}