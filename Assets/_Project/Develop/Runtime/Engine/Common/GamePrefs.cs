using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Common
{
    public static class GamePrefs
    {
        private const string MasterVolumeKey = "MasterVolume";
        private const string MusicVolumeKey = "MusicVolume";
        private const string EffectsVolumeKey = "EffectsVolume";

        private const float DefaultMasterVolume = 0.5f;
        private const float DefaultMusicVolume = 0.8f;
        private const float DefaultEffectsVolume = 0.8f;
        
        public static float GetMasterVolume()
        {
            return PlayerPrefs.GetFloat(MasterVolumeKey, DefaultMasterVolume);
        }

        public static void SetMasterVolume(float volume)
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, volume);
        }

        public static float GetMusicVolume()
        {
            return PlayerPrefs.GetFloat(MusicVolumeKey, DefaultMusicVolume);
        }

        public static void SetMusicVolume(float volume)
        {
            PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        }

        public static float GetEffectsVolume()
        {
            return PlayerPrefs.GetFloat(EffectsVolumeKey, DefaultEffectsVolume);
        }
        
        public static void SetEffectsVolume(float volume)
        {
            PlayerPrefs.SetFloat(EffectsVolumeKey, volume);
        }
    }
}