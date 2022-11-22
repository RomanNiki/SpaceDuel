using System;
using Components;
using Components.Tags;
using Components.Unit;
using Components.Unit.MoveComponents;
using Leopotam.Ecs;
using Pause;
using UnityEngine;
using Zenject;

namespace Systems.Unit
{
    public sealed class EnergyChargeSystem : IEcsRunSystem, IPauseHandler
    {
        [Inject] private readonly Settings _settings;
        private readonly EcsFilter<PlayerTag, Energy, Move> _playerFilter = null;
        private bool _isPause;

        public void Run()
        {
            if (_isPause)
                return;
            foreach (var i in _playerFilter)
            {
                ref var energy = ref _playerFilter.Get2(i);
                ref var view = ref _playerFilter.Get3(i);
                ref var entity = ref _playerFilter.GetEntity(i);
                
                ChargeEnergy(ref energy, CalculateChargeCoefficient(ref view) * _settings.ChargeAmount, ref entity);
            }
        }

        private static void ChargeEnergy(ref Energy energy, float amount, ref EcsEntity entity)
        {
            if (energy.InitialEnergy > energy.CurrentEnergy.Value)
            {
                energy.CurrentEnergy.Value += amount;
            }
            
            if (energy.CurrentEnergy.Value > 0f && entity.Has<NoEnergyBlock>())
            {
                entity.Del<NoEnergyBlock>();
            }
        }

        private float CalculateChargeCoefficient(ref Move view)
        {
            var rotationCoefficient = Vector3.Dot(view.LookDir,
                (Vector2)_settings.SunPosition - view.Position.normalized);
            if (rotationCoefficient > 0.1f)
            {
                return rotationCoefficient * CalculateDistanceCoefficient(ref view);
            }

            return 0f;
        }

        private float CalculateDistanceCoefficient(ref Move view)
        {
            var distance = Vector3.Distance(view.Position, _settings.SunPosition);
            return ScaleValue(_settings.MinChargeDistance, _settings.MaxChargeDistance, distance);
        }

        private static float ScaleValue(float min, float max, float value)
        {
            return (value - max) / (min - max);
        }

        public void SetPaused(bool isPaused)
        {
            _isPause = isPaused;
        }

        [Serializable]
        public class Settings
        {
            public Vector3 SunPosition;
            public float ChargeAmount;
            public float MaxChargeDistance;
            public float MinChargeDistance;
        }
    }
}