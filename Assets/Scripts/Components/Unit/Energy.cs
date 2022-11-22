using UniRx;

namespace Components.Unit
{
    public struct Energy
    {
        public float InitialEnergy;
        public ReactiveProperty<float> CurrentEnergy;
    }
}