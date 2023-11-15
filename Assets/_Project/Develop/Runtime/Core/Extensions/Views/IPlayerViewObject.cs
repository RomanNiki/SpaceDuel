using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Common.Enums;

namespace _Project.Develop.Runtime.Core.Extensions.Views
{
    public interface IPlayerViewObject : IViewObject
    {
        public void HealthChanged(Health health);
        public void EnergyChanged(Energy energy);
        public void Shoot(WeaponEnum weapon);
    }
}