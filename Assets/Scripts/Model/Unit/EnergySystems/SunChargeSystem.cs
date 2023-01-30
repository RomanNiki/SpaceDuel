using System;
using Leopotam.Ecs;
using Model.Components;
using Model.Extensions;
using Model.Unit.EnergySystems.Components;
using Model.Unit.EnergySystems.Components.Requests;
using Model.Unit.Movement.Components;
using Model.Unit.Movement.Components.Tags;
using UnityEngine;

namespace Model.Unit.EnergySystems
{
    public sealed class SunChargeSystem : PauseHandlerDefaultRunSystem
    {
        private readonly Settings _settings;
        private readonly EcsFilter<PlayerTag, Position, Rotation> _playerFilter = null;
        private readonly EcsFilter<Sun, Position, ChargeContainer> _sunFilter = null;

        protected override void Tick()
        {
            foreach (var j in _sunFilter)
            {
                ref var sunPosition = ref _sunFilter.Get2(j);
                ref var chargeAmount = ref _sunFilter.Get3(j).ChargeRequest.Value;
                foreach (var i in _playerFilter)
                {
                    ref var playerPosition = ref _playerFilter.Get2(i);
                    ref var playerRotation = ref _playerFilter.Get3(i);
                    ref var entity = ref _playerFilter.GetEntity(i);

                    entity.Get<ChargeRequest>().Value +=
                        CalculateChargeCoefficient(playerPosition, playerRotation, sunPosition) * chargeAmount;
                }
            }
        }

        private float CalculateChargeCoefficient(in Position playerPosition, in Rotation playerRotation,
            in Position sunPosition)
        {
            var rotationCoefficient = Vector3.Dot(playerRotation.LookDir,
                sunPosition.Value - playerPosition.Value.normalized);
            if (rotationCoefficient > 0.1f)
            {
                return rotationCoefficient * CalculateDistanceCoefficient(playerPosition, sunPosition);
            }

            return 0f;
        }

        private float CalculateDistanceCoefficient(in Position viewPosition, in Position sunTransform)
        {
            var distance = Vector3.Distance(viewPosition.Value, sunTransform.Value);
            return ScaleValue(_settings.MinChargeDistance, _settings.MaxChargeDistance, distance);
        }

        private static float ScaleValue(float min, float max, float value)
        {
            return (value - max) / (min - max);
        }

        [Serializable]
        public class Settings
        {
            public float MaxChargeDistance;
            public float MinChargeDistance;
        }
    }
}