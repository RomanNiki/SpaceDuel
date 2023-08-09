using EntityToGameObject;
using Model.Extensions.Pause;
using UnityEngine;

namespace Extensions.MappingUnityToModel
{
    [RequireComponent(typeof(EcsUnityProvider))]
    public class SpawnSoundPlayerUnityComponent : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private AudioSource _audioSource;

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