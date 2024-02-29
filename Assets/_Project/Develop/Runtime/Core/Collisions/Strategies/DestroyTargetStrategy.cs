using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Extensions;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Collisions.Strategies
{
    public class DestroyTargetStrategy : IEnterTriggerStrategy
    {
        public void OnEnter(World world, Entity sender, Entity target)
        {
            world.SendMessage(new KillRequest(){EntityToKill = target});
        }
    }
}