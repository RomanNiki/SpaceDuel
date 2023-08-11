using Scellecs.Morpeh;

namespace Core.Damage.Components
{
    public struct DyingPolicy : IComponent
    {
        public readonly IDyingPolicy Policy;

        public DyingPolicy(IDyingPolicy policy)
        {
            Policy = policy;
        }
    }
}