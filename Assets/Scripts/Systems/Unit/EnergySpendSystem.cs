using System;
using Components;
using Components.Tags;
using Components.Unit;
using Components.Unit.MoveComponents;
using Components.Unit.MoveComponents.Input;
using Enums;
using Leopotam.Ecs;
using Models;
using Models.Player.Weapon;
using UnityEngine;
using Zenject;

namespace Systems.Unit
{
    public sealed class EnergySpendSystem : IEcsRunSystem
    {
        private readonly Settings _settings;
        private readonly DefaultGun.Settings _firstWeaponSettings;
        private readonly DefaultGun.Settings _secondWeaponSettings;
        private readonly EcsFilter<PlayerTag, InputMoveData, InputShootData, Energy>.Exclude<NoEnergyBlock> _player = null;
        
        public EnergySpendSystem([Inject(Id = WeaponEnum.Primary)] DefaultGun.Settings firstWeaponSettings, [Inject(Id = WeaponEnum.Secondary)] DefaultGun.Settings secondWeaponSettings, [Inject] Settings settings)
        {
            _settings = settings;
            _firstWeaponSettings = firstWeaponSettings;
            _secondWeaponSettings = secondWeaponSettings;
        }
        
        public void Run()
        {
            foreach (var i in _player)
            {
                ref var inputData = ref _player.Get2(i);
                ref var shootData = ref _player.Get3(i);
                ref var energyComponent = ref _player.Get4(i);
                ref var entity = ref _player.GetEntity(i);
                
                if (inputData.Accelerate)
                {
                    SpendEnergy(ref energyComponent, ref entity, _settings.MoveCost);
                }

                if (Mathf.Abs(inputData.Rotation) > 0.02f)
                {
                    SpendEnergy(ref energyComponent, ref entity, _settings.RotationCost);
                }

                if (shootData.FirstShoot)
                {
                    SpendEnergy(ref energyComponent, ref entity, _firstWeaponSettings.EnergyCost);
                }  
                
                if (shootData.SecondShoot)
                {
                    SpendEnergy(ref energyComponent, ref entity, _secondWeaponSettings.EnergyCost);
                }
            }
        }
        
        private static void SpendEnergy(ref Energy energy, ref EcsEntity entity, in float energyLoss)
        {
            energy.CurrentEnergy.Value = Mathf.Max(0.0f, energy.CurrentEnergy.Value - energyLoss);
            if (energy.CurrentEnergy.Value <= 0f)
            {
                entity.Get<NoEnergyBlock>();
            }
        }
        
        [Serializable]
        public class Settings
        {
            public float RotationCost;
            public float MoveCost;
        }
    }
}