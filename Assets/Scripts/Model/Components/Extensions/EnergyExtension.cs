using Leopotam.Ecs;
using Model.Components.Unit;
using UnityEngine;

namespace Model.Components.Extensions
{
    public static class EnergyExtension
    {
        public static void SpendEnergy(this ref Energy energy, ref EcsEntity entity, in float energyLoss)
        {
            energy.CurrentEnergy = Mathf.Max(0.0f, energy.CurrentEnergy - energyLoss);
            if (energy.CurrentEnergy <= 0f)
            {
                entity.Get<NoEnergyBlock>();
            }
        }

        public static void ChargeEnergy(this ref Energy energy, ref EcsEntity entity, float amount)
        {
            if (energy.InitialEnergy > energy.CurrentEnergy)
            {
                energy.CurrentEnergy += amount;
            }

            if (energy.CurrentEnergy > 0f && entity.Has<NoEnergyBlock>())
            {
                entity.Del<NoEnergyBlock>();
            }
        }
    }
}