using Core.Characteristics.EnergyLimits.Components;
using Core.Extensions;
using Scellecs.Morpeh;

namespace Core.Collisions.Strategies
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