using Core.Characteristics.Damage.Components;
using Scellecs.Morpeh;

namespace Core.Collisions.Strategies
{
    public class GravityPointTriggerStrategy : IEnterTriggerStrategy
    {
        public void OnEnter(World world, Entity sender, Entity target)
        {
            var killSelfPool = world.GetStash<KillSelfRequest>();
            
            if (killSelfPool.Has(target) == false)
            {
                killSelfPool.Add(target);
            }
        }
    }
}