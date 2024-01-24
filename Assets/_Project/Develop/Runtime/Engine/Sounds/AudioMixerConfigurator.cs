using _Project.Develop.Runtime.Engine.Common;
using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds
{
    public class AudioMixerConfigurator : MonoBehaviour
    {
        private GameAudioMixer _mixer;
        public static AudioMixerConfigurator Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _mixer = GameAudioMixer.Instance;
            Configure();
        }

        public void Configure()
        {
            _mixer.SetVolume(SoundTypeEnum.Master, GamePrefs.GetMasterVolume());
            _mixer.SetVolume(SoundTypeEnum.Music, GamePrefs.GetMusicVolume());
            _mixer.SetVolume(SoundTypeEnum.Effects, GamePrefs.GetEffectsVolume());
        }
    }
}