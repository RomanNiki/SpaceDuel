using _Project.Develop.Runtime.Engine.Infrastructure.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.UI
{
    public class UISettingsPanel : MonoBehaviour
    {
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _effectsVolumeSlider;
        [SerializeField] private GameAudioMixer _mixer;

        private void OnEnable()
        {
            _masterVolumeSlider.value = _mixer.GetVolume(SoundTypeEnum.Master, false);
            _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderChanged);

            _musicVolumeSlider.value = _mixer.GetVolume(SoundTypeEnum.Music, false);
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);

            _effectsVolumeSlider.value = _mixer.GetVolume(SoundTypeEnum.Effects, false);
            _effectsVolumeSlider.onValueChanged.AddListener(OnEffectsVolumeSliderChanged);
        }

        private void OnDisable()
        {
            _masterVolumeSlider.onValueChanged.RemoveListener(OnMasterVolumeSliderChanged);
            _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeSliderChanged);
            _effectsVolumeSlider.onValueChanged.RemoveListener(OnEffectsVolumeSliderChanged);
        }

        private void OnMasterVolumeSliderChanged(float newValue)
        {
            _mixer.SetVolume(SoundTypeEnum.Master, newValue);
        }

        private void OnMusicVolumeSliderChanged(float newValue)
        {
            _mixer.SetVolume(SoundTypeEnum.Music, newValue);
        }

        private void OnEffectsVolumeSliderChanged(float newValue)
        {
            _mixer.SetVolume(SoundTypeEnum.Effects, newValue);
        }
    }
}