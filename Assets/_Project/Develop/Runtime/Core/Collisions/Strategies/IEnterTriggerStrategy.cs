using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Collisions.Strategies
{
    public interface IEnterTriggerStrategy
    {
        void OnEnter(World world, Entity sender, Entity target);
    }
}