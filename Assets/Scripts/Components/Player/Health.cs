using UniRx;

namespace Components.Player
{
    public struct Health
    {
        public float InitialHealth;
        public ReactiveProperty<float> CurrentHealth;
    }
}