using System;
using Components.Player;
using Components.Player.Move;
using Components.Player.MoveComponents;
using Extensions;
using Leopotam.Ecs;
using Models.Player;
using Tags;
using UniRx;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;
        [Inject] private PlayerModel.Settings _playerModelSettings;
        [Inject] private PlayerMover.Settings _moveSettings;
        [Inject] private PlayerInput _input;
        [Inject] private EcsWorld _world;

        private bool IsBlueTeam => _settings.Team == Team.Blue;

        public override void InstallBindings()
        {
            
          //  Container.Bind<InputActionMap>().FromInstance(IsBlueTeam ? _input.Player : _input.Player1);
            //Container.BindInterfacesTo<StandardDyingPolicy>().AsSingle().WhenInjectedInto<PlayerModel>();
            Container.Bind<AudioSource>().FromInstance(_settings.AudioSource);
            /*Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle()
                .WithArguments(_settings.Rigidbody, _settings.Team);
            Container.Bind<DefaultGun>().WithId(WeaponEnum.Primary).To<BulletGun>().AsSingle();
            Container.Bind<DefaultGun>().WithId(WeaponEnum.Secondary).To<MineGun>().AsSingle();
            Container.BindInterfacesAndSelfTo<DamageHandler>().AsSingle().WhenInjectedInto<PlayerPresenter>();
            Container.BindInterfacesAndSelfTo<PlayerShooter>().AsSingle();*/
            //Container.BindInterfacesAndSelfTo<PlayerInputRouter>().AsSingle();
            /*Container.BindInterfacesTo<PlayerMover>().AsSingle();
            Container.BindInterfacesTo<SolarCharger>().AsSingle();*/
            var entity = EcsInitPlayer();
            _settings.Rigidbody.transform.GetProvider().SetEntity(entity);
        }

        private EcsEntity EcsInitPlayer()
        {
            var entity = _world.NewEntity();
            entity.Get<PlayerTag>() = new PlayerTag();
            entity.Get<Move>().ViewObject = new ViewObjectUnity(_settings.Rigidbody, _moveSettings);
            entity.Get<InputRouter>() = new InputRouter(){InputRoute = new PlayerInputRouter(IsBlueTeam ? _input.Player : _input.Player1)};
            entity.Get<InputData>().Team = _settings.Team;
            entity.Get<Health>() = new Health()
            {
                InitialHealth = _playerModelSettings.MaxHealth,
                CurrentHealth = new ReactiveProperty<float>(_playerModelSettings.MaxHealth)
            };
            entity.Get<Energy>() = new Energy()
            {
                InitialEnergy = _playerModelSettings.MaxHealth,
                CurrentEnergy = new ReactiveProperty<float>(_playerModelSettings.MaxHealth)
            };
           

            return entity;
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