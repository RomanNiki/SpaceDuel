using System;
using Models;
using Models.Player;
using Models.Player.DyingPolicies;
using Models.Player.Interfaces;
using Models.Player.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            var input = new PlayerInput();
            Container.Bind<InputActionMap>().FromInstance(_settings.Team == Team.Blue ? input.Player : input.Player1);
            Container.Bind<IDyingPolicy>().To<StandardDyingPolicy>().AsSingle();
            Container.Bind<AudioSource>().FromInstance(_settings.AudioSource);
            Container.Bind<PlayerModel>().AsSingle()
                .WithArguments(_settings.Rigidbody, _settings.MeshRenderer, _settings.Team);
            Container.Bind<DefaultGun>().WithId(WeaponEnum.Primary).To<BulletGun>().AsSingle();
            Container.Bind<DefaultGun>().WithId(WeaponEnum.Secondary).To<MineGun>().AsSingle();
            Container.Bind<DamageHandler>().AsSingle();
            Container.Bind<PlayerShooter>().AsSingle();
            Container.Bind<PlayerInputRouter>().AsSingle();
            Container.BindInterfacesTo<PlayerMover>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody2D Rigidbody;
            public MeshRenderer MeshRenderer;
            public AudioSource AudioSource;
            public Team Team;
        }
    }
}