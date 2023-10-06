using System;
using System.Threading.Tasks;
using Core.Services.Time;
using UnityEngine;

namespace Engine.Services.Time
{
    public class BaseTimeScale : ITimeScale
    {
        public float TimeScale { get; private set; } = 1f;
        
        public async Task SlowDown(float target, float duration = 3f)
        {
            if (target > TimeScale)
            {
                throw new ArgumentException("Target value can't be more then current value");
            }

            await Lerp(target, duration);
        }

        private async Task Lerp(float target, float duration)
        {
            for (var t = 0f; t < duration; t += UnityEngine.Time.deltaTime)
            {
                var normalizedTime = t / duration;
                TimeScale = Mathf.Lerp(TimeScale, target, normalizedTime);
                await Task.Yield();
            }
        }

        public async Task Accelerate(float target, float duration = 3f)
        {
            if (target < TimeScale)
            {
                throw new ArgumentException("Current value can't be more then current target");
            }

            await Lerp(target, duration);
        }
    }
}