using Core.Characteristics.Damage.Components;
using Core.Extensions;
using Core.Movement.Components;
using Scellecs.Morpeh;

namespace Core.Collisions.Strategies
{
    public class DamageTargetByHealthStrategy : IEnterTriggerStrategy
    {
        public void OnEnter(World world, Entity sender, Entity target)
        {
            var healthPool = world.GetStash<Health>();
            var positionPool = world.GetStash<Position>();
            world.SendMessage(new DamageRequest(healthPool.Get(sender).Value, positionPool.Get(sender).Value, target));
        }
    }
}