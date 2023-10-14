using System;
using System.Globalization;
using Core.Services.Time;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Engine.Services.Time
{
    public class BaseTimeScale : ITimeScale
    {
        public float TimeScale { get; private set; } = 1f;
        
        public async UniTask SlowDown(float target, float duration = 3f)
        {
            if (target > TimeScale)
            {
                throw new ArgumentException("Target value can't be more then current value");
            }

            await Lerp(target, duration);
        }

        private async UniTask Lerp(float target, float duration)
        {
            for (var elapsedTime = 0f; elapsedTime < duration; elapsedTime += UnityEngine.Time.deltaTime)
            {
                var normalizedTime = Mathf.Clamp01(elapsedTime / duration);
                TimeScale = Mathf.Lerp(TimeScale, target, normalizedTime);
                await UniTask.Yield();
            }
        }

        public async UniTask Accelerate(float target, float duration = 3f)
        {
            if (target < TimeScale)
            {
                throw new ArgumentException("Current value can't be more then current target");
            }

            await Lerp(target, duration);
        }
    }
}