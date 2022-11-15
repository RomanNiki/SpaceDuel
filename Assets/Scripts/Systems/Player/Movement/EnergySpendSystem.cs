using Components;
using Components.Player;
using Components.Player.MoveComponents;
using Leopotam.Ecs;
using Models;
using Models.Player;
using Models.Player.Weapon;
using Tags;
using UnityEngine;
using Zenject;

namespace Systems.Player.Movement
{
    public sealed class EnergySpendSystem : IEcsRunSystem
    {
        private readonly PlayerMover.Settings _moveSettings;
        private readonly DefaultGun.Settings _firstWeaponSettings;
        private readonly DefaultGun.Settings _secondWeaponSettings;
        private readonly EcsFilter<PlayerTag, InputData, Energy>.Exclude<NoEnergyBlock> _player = null;
        
        public EnergySpendSystem([Inject(Id = WeaponEnum.Primary)] DefaultGun.Settings firstWeaponSettings, [Inject(Id = WeaponEnum.Secondary)] DefaultGun.Settings secondWeaponSettings, [Inject] PlayerMover.Settings moveSettings)
        {
            _moveSettings = moveSettings;
            _firstWeaponSettings = firstWeaponSettings;
            _secondWeaponSettings = secondWeaponSettings;
        }
        
        public void Run()
        {
            foreach (var i in _player)
            {
                ref var inputData = ref _player.Get2(i);
                ref var energyComponent = ref _player.Get3(i);
                ref var entity = ref _player.GetEntity(i);
                
                if (inputData.Accelerate)
                {
                    SpendEnergy(ref energyComponent, ref entity, _moveSettings.MoveCost);
                }

                if (Mathf.Abs(inputData.Rotation) > 0)
                {
                    SpendEnergy(ref energyComponent, ref entity, _moveSettings.RotationCost);
                }

                if (inputData.FirstShoot)
                {
                    SpendEnergy(ref energyComponent, ref entity, _firstWeaponSettings.EnergyCost);
                }  
                
                if (inputData.SecondShoot)
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
    }
}