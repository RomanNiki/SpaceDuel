namespace Player
{
    public class PlayerModel
    {
        public PlayerModel(float health)
        {
            Health = health;
        }

        public float Health
        {
            get; private set;
        }
    }
}