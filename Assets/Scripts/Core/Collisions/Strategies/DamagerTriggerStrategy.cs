using Core.Characteristics.Damage.Components;
using Core.Extensions;
using Core.Movement.Components;
using Core.Timers.Components;
using Scellecs.Morpeh;

namespace Core.Collisions.Strategies
{
    public class DamagerTriggerStrategy : IEnterTriggerStrategy
    {
        public void OnEnter(World world, Entity sender, Entity target)
        {
            var invisibleTimers = world.GetStash<Timer<InvisibleTimer>>();
            
            if (invisibleTimers.Has(sender))
            {
                return;
            }
            
            var killSelfRequest = world.GetStash<KillSelfRequest>();
            var damages = world.GetStash<DamageContainer>();
            var positions = world.GetStash<Position>();
            var damage = damages.Get(sender);
            var position = positions.Get(sender);
           
            if (killSelfRequest.Has(sender) == false)
            {
                killSelfRequest.Add(sender);
            }
            
            world.SendMessage(new DamageRequest(damage.Value, position.Value, target));
        }
    }
}