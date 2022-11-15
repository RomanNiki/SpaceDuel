
namespace Models.Player.Interfaces
{
    public interface IEnergyCharger : IEnergyContainer
    {
        void ChargeEnergy(float count);
    }
}