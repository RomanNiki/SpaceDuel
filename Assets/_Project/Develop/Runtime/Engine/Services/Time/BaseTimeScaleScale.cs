using System;
using _Project.Develop.Runtime.Core.Services.Time;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Time
{
    public class BaseTimeScale : ITimeScale
    {
        private readonly float _defaultTimeScale;
        public float TimeScale { get; private set; }

        public BaseTimeScale(float defaultTimeScale = 1f)
        {
            _defaultTimeScale = TimeScale = defaultTimeScale;
        }

        public async UniTask SlowDown(float target, float duration)
        {
            if (target > TimeScale)
            {
                throw new ArgumentException("Target value can't be more then current value");
            }

            await Lerp(target, duration);
        }

        public async UniTask Accelerate(float target, float duration)
        {
            TimeScale = duration switch
            {
                < 0f => throw new ArgumentOutOfRangeException("duration", "Cannot be less then zero"),
                < 0.1f => target,
                _ => TimeScale
            };

            if (target < TimeScale)
                throw new ArgumentException("Current value can't be more then current target");


            await Lerp(target, duration);
        }

        public void SetTimeScale(float target)
        {
            TimeScale = target;
        }

        private async UniTask Lerp(float target, float duration)
        {
            if (Mathf.Approximately(duration, 0f) || Mathf.Approximately(TimeScale, target))
            {
                TimeScale = target;
                return;
            }

            for (var elapsedTime = 0f; elapsedTime < duration; elapsedTime += UnityEngine.Time.deltaTime)
            {
                var normalizedTime = Mathf.Clamp01(elapsedTime / duration);
                TimeScale = Mathf.Lerp(TimeScale, target, normalizedTime);
                await UniTask.Yield();
            }

            TimeScale = target;
        }

        public void Reset()
        {
            TimeScale = _defaultTimeScale;
            TimeScale = _defaultTimeScale;
        }
    }
}