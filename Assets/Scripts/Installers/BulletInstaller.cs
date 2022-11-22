using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BulletInstaller : MonoInstaller<BulletInstaller>
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            /*Container.BindInterfacesAndSelfTo<DamagerModel>().AsSingle().WithArguments(_settings.Rigidbody);
           // Container.BindInterfacesTo<BulletMover>().AsSingle();
            Container.BindInterfacesTo<EnergySpender>().AsSingle();*/
        }

        [Serializable]
        public class Settings
        {  
            public float Health;
            public float Energy;
            public float Damage;
            public Rigidbody2D Rigidbody;
        }
    }
}