using Core.Buffs.Components;
using Core.Extensions;
using Scellecs.Morpeh;

namespace Core.Collisions.Strategies
{
    public class CreateBuffStrategy : IEnterTriggerStrategy
    {
        public void OnEnter(World world, Entity sender, Entity target)
        {
            world.SendMessage(new BuffRequest(sender, target));
        }
    }
}