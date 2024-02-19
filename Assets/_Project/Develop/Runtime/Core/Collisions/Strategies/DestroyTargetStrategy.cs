using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Collisions.Strategies
{
    public class DestroyTargetStrategy : IEnterTriggerStrategy
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