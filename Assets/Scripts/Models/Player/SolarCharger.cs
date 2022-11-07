﻿using System;
using Models.Pause;
using UnityEngine;
using Zenject;

namespace Models.Player
{
    public sealed class SolarCharger : ITickable, IPauseHandler

    {
        private readonly PlayerModel _playerModel;
        private readonly Settings _settings;
        private bool _isPause;

        public SolarCharger(PlayerModel playerModel, Settings settings)
        {
            _playerModel = playerModel;
            _settings = settings;
        }

        public void Tick()
        {
            if (_isPause)
                return;
            _playerModel.ChargeEnergy(CalculateChargeCoefficient() * _settings.ChargeAmount);
        }

        private float CalculateChargeCoefficient()
        {
            var rotationCoefficient = Vector3.Dot(_playerModel.LookDir,
                _settings.SunPosition - _playerModel.Position.normalized);
            if (rotationCoefficient > 0.1f)
            {
                return rotationCoefficient * CalculateDistanceCoefficient();
            }

            return 0f;
        }

        private float CalculateDistanceCoefficient()
        {
            var distance = Vector3.Distance(_playerModel.Position, _settings.SunPosition);
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