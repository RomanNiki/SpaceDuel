using System;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Components.Tags;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents;
using Model.Pause;
using UnityEngine;
using Zenject;

namespace Model.Systems.Unit
{
    public sealed class SunChargeSystem : IEcsRunSystem, IPauseHandler
    {
        [Inject] private readonly Settings _settings;
        private readonly EcsFilter<PlayerTag, TransformData> _playerFilter = null;
        private readonly EcsFilter<Sun, TransformData, ChargeContainer> _sunFilter = null;
        private bool _isPause;

        public void Run()
        {
            if (_isPause)
                return;
            foreach (var j in _sunFilter)
            {
                ref var sunTransform = ref _sunFilter.Get2(j);
                ref var chargeAmount = ref _sunFilter.Get3(j).ChargeRequest.Value;
                foreach (var i in _playerFilter)
                {
                    ref var playerTransform = ref _playerFilter.Get2(i);
                    ref var entity = ref _playerFilter.GetEntity(i);

                    entity.Get<ChargeRequest>().Value +=
                        CalculateChargeCoefficient(ref playerTransform, ref sunTransform) * chargeAmount;
                }
            }
        }

        private float CalculateChargeCoefficient(ref TransformData playerTransform, ref TransformData sunTransform)
        {
            var rotationCoefficient = Vector3.Dot(playerTransform.LookDir,
                sunTransform.Position - playerTransform.Position.normalized);
            if (rotationCoefficient > 0.1f)
            {
                return rotationCoefficient * CalculateDistanceCoefficient(ref playerTransform, ref sunTransform);
            }

            return 0f;
        }

        private float CalculateDistanceCoefficient(ref TransformData view, ref TransformData sunTransform)
        {
            var distance = Vector3.Distance(view.Position, sunTransform.Position);
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
            public float MaxChargeDistance;
            public float MinChargeDistance;
        }
    }
}