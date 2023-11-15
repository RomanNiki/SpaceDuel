using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Movement.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Collisions.Strategies
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