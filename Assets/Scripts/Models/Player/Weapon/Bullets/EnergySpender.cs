using System;
using Models.Player.Interfaces;
using Pause;
using UnityEngine;
using Zenject;

namespace Models.Player.Weapon.Bullets
{
    public class EnergySpender : ITickable, IPauseHandler
    {
        private readonly IEnergyContainer _energyContainer;
        private bool _isPause;
        
        public EnergySpender(IEnergyContainer energyContainer)
        {
            _energyContainer = energyContainer;
        }
        
        public void Tick()
        {
            if (_energyContainer.Energy.Value > 0f || _isPause == false)
            {
                _energyContainer.SpendEnergy(Time.deltaTime);
            }
        }

        public void SetPaused(bool isPaused)
        {
            _isPause = isPaused;
        }
    }
}