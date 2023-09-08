using Core.Characteristics.Damage.Components;
using Core.Characteristics.EnergyLimits.Components;
using Core.Characteristics.Enums;

namespace Core.Extensions.Views
{
    public interface IPlayerViewObject : IViewObject
    {
        public void HealthChanged(Health health);
        public void EnergyChanged(Energy energy);
        public void Shoot(WeaponEnum weapon);
    }
}