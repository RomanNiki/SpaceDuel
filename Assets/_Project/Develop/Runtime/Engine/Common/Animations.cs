﻿using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Common
{
    public static class Animations
    {
        public static async UniTask Fade(this CanvasGroup target, float endValue, float duration,
            CancellationToken token = default)
        {
            try
            {
                var startValue = target.alpha;
                for (var i = startValue; i <= endValue; i += 1f / duration)
                {
                    target.alpha = Mathf.Lerp(startValue, endValue, i);
                    await UniTask.Yield();
                }

                target.alpha = endValue;
            }
            catch when (token.IsCancellationRequested)
            {
            }
        }

        private static async UniTask Lerp(this float item, float target, float duration)
        {
            for (var t = 0f; t < duration; t += UnityEngine.Time.deltaTime)
            {
                var normalizedTime = t / duration;
                item = Mathf.Lerp(item, target, normalizedTime);
                await UniTask.Yield();
            }
        }
    }
}