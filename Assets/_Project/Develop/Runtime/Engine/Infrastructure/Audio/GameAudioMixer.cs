using System;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Engine.Infrastructure.Audio
{
    public class GameAudioMixer
    {
        private readonly Dictionary<SoundTypeEnum, float> _volumes = new();

        private GameAudioMixer()
        {
        }
        
        public static GameAudioMixer Instance { get; } = new();
        
        public event Action<SoundTypeEnum> VolumeChanged;

        public float GetVolume(SoundTypeEnum typeEnum)
        {
            if (_volumes.TryGetValue(SoundTypeEnum.Master, out var masterVolume))
            {
                if (SoundTypeEnum.Master != typeEnum)
                {
                    return _volumes[typeEnum] * masterVolume;
                }

                return masterVolume;
            }

            return _volumes[typeEnum];
        }

        public void SetVolume(SoundTypeEnum typeEnum, float targetVolume)
        {
            if (_volumes.TryGetValue(typeEnum, out _))
            {
                _volumes[typeEnum] = targetVolume;
            }
            else
            {
                _volumes.Add(typeEnum, targetVolume);
            }

            VolumeChanged?.Invoke(typeEnum);
        }
    }
}