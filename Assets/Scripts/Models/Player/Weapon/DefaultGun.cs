using System;
using UnityEngine;

namespace Models.Player.Weapon
{
    public abstract class DefaultGun
    {
        protected readonly AudioSource AudioSource;
        protected float LastFireTime;
        protected readonly PlayerModel PlayerModel;

        protected DefaultGun(AudioSource audioSource, PlayerModel playerModel)
        {
            AudioSource = audioSource;
            PlayerModel = playerModel;
        }

        public abstract bool CanShoot();

        public void Shoot()
        {
            PlaySound();
            InitBullet();
            LastFireTime = Time.realtimeSinceStartup;
        }

        protected abstract void PlaySound();

        protected abstract void InitBullet();
        
        [Serializable]
        public class Settings
        {
            public AudioClip ShootSound;
            public float EnergyCost;
            public float ShootSoundVolume;
            public float StartForce;
            public float MaxShootInterval;
            public float SpawnOffset;
        }
    }
}