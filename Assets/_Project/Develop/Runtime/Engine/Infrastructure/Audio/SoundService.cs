using System.Collections.Generic;
using _Project.Develop.Runtime.Engine.Infrastructure.Audio.Interfaces;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Infrastructure.Audio
{
    public class SoundService : ISoundService
    {
        private readonly bool _usePlayerPrefs;
        
        public SoundService(bool usePlayerPrefs)
        {
            _usePlayerPrefs = usePlayerPrefs;
        }
        
        public void Load(List<VolumeData> volumeDatas)
        {
            if (_usePlayerPrefs)
            {
                foreach (var volumeData in volumeDatas)
                {
                    volumeData.Volume = PlayerPrefs.GetFloat(volumeData.Key, volumeData.Volume);
                }
            }
        }

        public void Save(List<VolumeData> volumeDatas)
        {
            if (_usePlayerPrefs)
            {
                foreach (var volumeData in volumeDatas)
                {
                    PlayerPrefs.SetFloat(volumeData.Key, volumeData.Volume);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}