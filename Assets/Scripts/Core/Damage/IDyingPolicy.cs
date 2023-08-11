namespace Core.Damage
{
    public interface IDyingPolicy
    {
        public bool CheckDeath(float currentHealth);
    }
}