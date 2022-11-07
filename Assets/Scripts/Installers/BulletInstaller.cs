﻿using System;
using Models.Player.Weapon.Bullets;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BulletInstaller : MonoInstaller<BulletInstaller>
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DamagerModel>().AsSingle().WithArguments(_settings.Rigidbody);
            Container.BindInterfacesTo<BulletMover>().AsSingle();
            Container.BindInterfacesTo<EnergySpender>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody2D Rigidbody;
        }
    }
}