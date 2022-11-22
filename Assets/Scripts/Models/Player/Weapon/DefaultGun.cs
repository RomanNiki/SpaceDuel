using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Models.Player.Weapon
{
    public abstract class DefaultGun
    {
        protected readonly EcsEntity Weapon;

        protected DefaultGun(ref EcsEntity weapon)
        {
            Weapon = weapon;
        }
        
        public abstract EcsEntity SpawnBullet();

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