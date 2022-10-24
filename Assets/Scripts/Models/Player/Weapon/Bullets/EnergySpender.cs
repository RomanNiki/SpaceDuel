using Models.Player.Interfaces;
using UnityEngine;
using Zenject;

namespace Models.Player.Weapon.Bullets
{
    public class EnergySpender : ITickable
    {
        private readonly IEnergyContainer _energyContainer;

        public EnergySpender(IEnergyContainer energyContainer)
        {
            _energyContainer = energyContainer;
        }
        
        public void Tick()
        {
            if (_energyContainer.Energy.Value > 0f)
            {
                _energyContainer.SpendEnergy(Time.deltaTime);
            }
        }
    }
}