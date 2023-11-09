using Core.Characteristics.Damage.Components;
using Scellecs.Morpeh;

namespace Core.Collisions.Strategies
{
    public class KillSenderStrategy : IEnterTriggerStrategy
    {
        public void OnEnter(World world, Entity sender, Entity target)
        {
            var killSelfRequest = world.GetStash<KillSelfRequest>();
            if (killSelfRequest.Has(sender) == false)
            {
                killSelfRequest.Add(sender);
            }
        }
    }
}