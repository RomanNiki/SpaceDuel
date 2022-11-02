using Models.Player.Interfaces;

namespace Models.Player.DyingPolicies
{
    public sealed class StandardDyingPolicy : IDyingPolicy
    {
        public bool Died(float value)
        {
            return value <= 0;
        }
    }
}