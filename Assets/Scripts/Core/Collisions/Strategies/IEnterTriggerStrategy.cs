using Scellecs.Morpeh;

namespace Core.Collisions.Strategies
{
    public interface IEnterTriggerStrategy
    {
        void OnEnter(World world, Entity sender, Entity target);
    }
}