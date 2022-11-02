using UniRx;

namespace Models.Player.Interfaces
{
    public interface IEnergyContainer
    {
        IReadOnlyReactiveProperty<float> Energy { get; }
        void SpendEnergy(float count);
        void ChargeEnergy(float count);
    }
}