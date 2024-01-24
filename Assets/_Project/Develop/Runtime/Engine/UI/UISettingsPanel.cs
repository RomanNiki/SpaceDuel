using _Project.Develop.Runtime.Engine.Common;
using _Project.Develop.Runtime.Engine.Sounds;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.Engine.UI
{
    public class UISettingsPanel : MonoBehaviour
    {
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _effectsVolumeSlider;
        private AudioMixerConfigurator _audioMixerConfigurator;

        private void Start()
        {
            _audioMixerConfigurator = AudioMixerConfigurator.Instance;
        }

        private void OnEnable()
        {
            _masterVolumeSlider.value = GamePrefs.GetMasterVolume();
            _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderChanged);
           
            _musicVolumeSlider.value = GamePrefs.GetMusicVolume();
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);   
            
            _effectsVolumeSlider.value = GamePrefs.GetEffectsVolume();
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
            GamePrefs.SetMasterVolume(newValue);
            _audioMixerConfigurator.Configure();
        }

        private void OnMusicVolumeSliderChanged(float newValue)
        {
            GamePrefs.SetMusicVolume(newValue);
            _audioMixerConfigurator.Configure();
        } 
        
        private void OnEffectsVolumeSliderChanged(float newValue)
        {
            GamePrefs.SetEffectsVolume(newValue);
            _audioMixerConfigurator.Configure();
        }
        
    }
}