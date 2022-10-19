using System;
using UnityEngine;

namespace Models.Player.Weapon
{
    public class DefaultGun
    {
        private readonly AudioSource _audioSource;
        private readonly Settings _settings;

        public DefaultGun(AudioSource audioSource, Settings settings)
        {
            _audioSource = audioSource;
            _settings = settings;
        }
        
        public virtual bool CanShoot()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Shoot()
        {
            _audioSource.PlayOneShot(_settings.ShootSound, _settings.ShootSoundVolume);
            throw new System.NotImplementedException();
        }
        
        [Serializable]
        public class Settings
        {
            public AudioClip ShootSound;
            public float ShootSoundVolume;
        }
    }
}