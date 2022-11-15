using UniRx;

namespace Components.Player
{
    public struct Energy
    {
        public float InitialEnergy;
        public ReactiveProperty<float> CurrentEnergy;
        
        
    }
}