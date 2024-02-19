using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Engine.Infrastructure.Audio.Interfaces;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Infrastructure.Audio
{
    [CreateAssetMenu(menuName = "SpaceDuel/AudioMixer", fileName = nameof(GameAudioMixer))]
    public class GameAudioMixer : ScriptableObject
    {
        [SerializeField] private bool _usePlayerPrefs;
        [SerializeField] private List<VolumeData> _volumeDataList;
        public event Action<SoundTypeEnum> VolumeChanged;
        private ISoundService _soundService;

        private void Awake()
        {
            _soundService = new SoundService(_usePlayerPrefs);
            Configure();
        }

        private void Configure()
        {
            _soundService.Load(_volumeDataList);
        }

        public float GetVolume(SoundTypeEnum typeEnum, bool scaled = true)
        {
            var masterVolume = _volumeDataList.First(v => v.Type == SoundTypeEnum.Master).Volume;

            if (typeEnum == SoundTypeEnum.Master) return masterVolume;

            var specificVolume = _volumeDataList.First(v => v.Type == typeEnum).Volume;
            if (scaled)
            {
                return specificVolume * masterVolume;
            }

            return specificVolume;
        }

        public void SetVolume(SoundTypeEnum typeEnum, float targetVolume)
        {
            var existingVolume = _volumeDataList.FirstOrDefault(v => v.Type == typeEnum);
            if (existingVolume != null)
            {
                existingVolume.Volume = targetVolume;
            }
            else
            {
                _volumeDataList.Add(new VolumeData { Type = typeEnum, Volume = targetVolume });
            }

            VolumeChanged?.Invoke(typeEnum);
        }

        private void OnDestroy()
        {
            _soundService?.Save(_volumeDataList);
            _soundService?.Dispose();
        }
    }

    [Serializable]
    public class VolumeData
    {
        public SoundTypeEnum Type;
        [Range(0f, 1f)] public float Volume;
        public string Key = "MasterVolume";

        public VolumeData()
        {
        }

        public VolumeData(SoundTypeEnum type)
        {
            Type = type;
            Volume = 0.5f;
        }
    }
}