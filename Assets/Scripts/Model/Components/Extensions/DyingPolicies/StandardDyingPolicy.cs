namespace Model.Components.Extensions.DyingPolicies
{
    public sealed class StandardDyingPolicy : IDyingPolicy
    {
        public bool Died(float value)
        {
            return value <= 0;
        }
    }
}