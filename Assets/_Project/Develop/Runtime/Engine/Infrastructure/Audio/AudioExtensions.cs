using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Infrastructure.Audio
{
    public static class AudioExtensions
    {
        private const float VolumeLog10Multiplier = 20;

        public static float GetVolumeInDecibels(float volume)
        {
            if (volume <= 0)
            {
                volume = 0.0001f;
            }

            return Mathf.Log10(volume) * VolumeLog10Multiplier;
        }

        public static float FromDecibelsToVolume(float decibels)
        {
            return Mathf.Pow(10, decibels / VolumeLog10Multiplier);
        }

        public static async UniTask AmplifyAudioSource(GameAudioSource audioSource, float durations = 1f,
            CancellationToken token = default)
        {
            try
            {
                await VolumeTransition(audioSource, durations, true, token);
            }
            catch when (token.IsCancellationRequested)
            {
            }
        }


        public static async UniTask QuietenAudioSource(GameAudioSource audioSource, float durations = 1f,
            CancellationToken token = default)
        {
            try
            {
                await VolumeTransition(audioSource, durations, false, token);
                audioSource.Stop();
            }
            catch when (token.IsCancellationRequested)
            {
            }
        }

        private static async UniTask VolumeTransition(GameAudioSource audioSource, float durations = 1f,
            bool isAmplify = true,
            CancellationToken token = default)
        {
            var elapsedTime = 0f;
            while (elapsedTime < durations)
            {
                var targetVolume = elapsedTime / durations;
                if (isAmplify == false)
                {
                    targetVolume = 1 - targetVolume;
                }

                audioSource.SetVolumeFactor(targetVolume);
                elapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }
        }
    }
}