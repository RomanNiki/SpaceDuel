using System;
using System.Collections.Generic;
using System.Threading;
using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using _Project.Develop.Runtime.Engine.Sounds.Ambient.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds.Ambient
{
    public class AmbientSoundController : MonoBehaviour, IAmbientSoundController
    {
        [SerializeField] private GameAudioSource _firstAudioSource;
        [SerializeField] private GameAudioSource _secondAudioSource;
        [SerializeField] private List<AmbientClip> _ambientClips;
        [SerializeField] private float _fadeDuration = 0.3f;
        private CancellationTokenSource _nowPlayTokenSource;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void SetPaused(bool isPaused)
        {
            var ambientEnum = isPaused ? AmbientEnum.Pause : AmbientEnum.Game;
            if (TryGetValue(ambientEnum, out var clip))
            {
                FadeTrack(clip, _fadeDuration);
            }
            else
            {
                throw new NullReferenceException($"{nameof(_ambientClips)} dont have {ambientEnum.ToString()} Id");
            }
        }

        public void PlayMenuAmbient()
        {
            if (TryGetValue(AmbientEnum.Menu, out var clip))
            {
                FadeTrack(clip, _fadeDuration);
            }
            else
            {
                throw new NullReferenceException($"{nameof(_ambientClips)} dont have Menu Id");
            }
        }

        public void PlayGameAmbient()
        {
            if (TryGetValue(AmbientEnum.Game, out var clip))
            {
                FadeTrack(clip, _fadeDuration);
            }
            else
            {
                throw new NullReferenceException($"{nameof(_ambientClips)} dont have Game Id");
            }
        }

        private bool TryGetId(AmbientEnum id, out AmbientClip value)
        {
            foreach (var info in _ambientClips)
            {
                if (info.Id == id)
                {
                    value = info;
                    return true;
                }
            }

            value = default;
            return false;
        }

        private bool TryGetValue(AmbientEnum id, out AudioClip value)
        {
            if (TryGetId(id, out var clip) == false)
            {
                value = default;
                return false;
            }

            value = clip.Clip;
            return true;
        }

        private void FadeTrack(AudioClip clip, float fadeDuration)
        {
            if (_nowPlayTokenSource != null)
            {
                _nowPlayTokenSource.Cancel();
                _nowPlayTokenSource.Dispose();
            }

            _nowPlayTokenSource = new CancellationTokenSource();
            var linkedTokenSource =
                CancellationTokenSource.CreateLinkedTokenSource(_nowPlayTokenSource.Token, destroyCancellationToken);

            if (_firstAudioSource.IsPlaying)
            {
                FadeAmbientTrack(_firstAudioSource, _secondAudioSource, clip, fadeDuration,
                    linkedTokenSource.Token).Forget();
            }
            else
            {
                FadeAmbientTrack(_secondAudioSource, _firstAudioSource, clip, fadeDuration,
                    linkedTokenSource.Token).Forget();
            }

            _nowPlayTokenSource.Dispose();
            _nowPlayTokenSource = null;
        }

        private static async UniTaskVoid FadeAmbientTrack(GameAudioSource toMute, GameAudioSource toPlay,
            AudioClip clip,
            float fadeDuration, CancellationToken token = default)
        {
            try
            {
                toPlay.Clip = clip;
                toPlay.Play();
                var elapsedTime = 0f;
                while (elapsedTime < fadeDuration)
                {
                    toMute.SetVolumeFactor(Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration));
                    toPlay.SetVolumeFactor(Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration));
                    elapsedTime += Time.deltaTime;
                    await UniTask.Yield();
                }

                toMute.Stop();
            }
            catch when (token.IsCancellationRequested)
            {
            }
        }
    }
}