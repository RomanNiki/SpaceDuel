using Core.Enums;
using Core.Player.Components;

namespace Core.Extensions.Views
{
    public interface IPlayerViewObject : IViewObject
    {
        public void HealthChanged(Health health);
        public void EnergyChanged(Energy energy);
        public void Shoot(WeaponEnum weapon);
    }
}