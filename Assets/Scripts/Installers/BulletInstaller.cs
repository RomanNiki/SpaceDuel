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
            Container.Bind<DamagerModel>().AsSingle().WithArguments(_settings.Rigidbody);
            Container.BindInterfacesTo<BulletMover>().AsSingle();
            Container.Bind<EnergySpender>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody2D Rigidbody;
        }
    }
}