using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Extensions;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Collisions.Strategies
{
    public class ChargeTargetStrategy : IEnterTriggerStrategy
    {
        public void OnEnter(World world, Entity sender, Entity target)
        {
            var chargePool = world.GetStash<ChargeContainer>();
            ref var chargeContainer = ref chargePool.Get(sender);
            world.SendMessage(new ChargeRequest{Entity = target, Value = chargeContainer.Value});
        }
    }
}