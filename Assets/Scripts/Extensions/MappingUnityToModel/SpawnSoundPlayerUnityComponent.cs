using EntityToGameObject;
using JetBrains.Annotations;
using Leopotam.Ecs;
using Model.Components;
using Model.Extensions.Pause;
using UnityEngine;

namespace Extensions.MappingUnityToModel
{
    [RequireComponent(typeof(EcsUnityProvider))]
    public class SpawnSoundPlayerUnityComponent : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] [CanBeNull] private AudioClip _audioClip;
        private EcsUnityProvider _provider;

        private void Awake()
        {
            _provider = GetComponent<EcsUnityProvider>();
            if (_audioClip != null) return;
            if (_provider.Entity.Has<UnityComponent<AudioClip>>())
            {
                _audioClip = _provider.Entity.Get<UnityComponent<AudioClip>>().Value;
            }
        }

        private void OnEnable()
        {
            _audioSource.PlayOneShot(_audioClip, 0.25f);
        }

        public void SetPaused(bool isPaused)
        {
            if (isPaused)
            {
                _audioSource.Pause();
            }

            _audioSource.UnPause();
        }
    }
}