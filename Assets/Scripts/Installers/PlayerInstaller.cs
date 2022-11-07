using System;
using Models;
using Models.Player;
using Models.Player.DyingPolicies;
using Models.Player.Weapon;
using Presenters;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;
        [Inject] private PlayerInput _input;

        public override void InstallBindings()
        {
            Container.Bind<InputActionMap>().FromInstance(_settings.Team == Team.Blue ? _input.Player : _input.Player1);
            Container.BindInterfacesTo<StandardDyingPolicy>().AsSingle().WhenInjectedInto<PlayerModel>();
            Container.Bind<AudioSource>().FromInstance(_settings.AudioSource);
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle().WithArguments(_settings.Rigidbody, _settings.Team);
            Container.Bind<DefaultGun>().WithId(WeaponEnum.Primary).To<BulletGun>().AsSingle();
            Container.Bind<DefaultGun>().WithId(WeaponEnum.Secondary).To<MineGun>().AsSingle();
            Container.BindInterfacesAndSelfTo<DamageHandler>().AsSingle().WhenInjectedInto<PlayerPresenter>();
            Container.BindInterfacesAndSelfTo<PlayerShooter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputRouter>().AsSingle();
            Container.BindInterfacesTo<PlayerMover>().AsSingle();
            Container.BindInterfacesTo<SolarCharger>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody2D Rigidbody;
            public AudioSource AudioSource;
            public Team Team;
        }
    }
}