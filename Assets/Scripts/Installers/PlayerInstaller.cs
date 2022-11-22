using System;
using Components.Extensions.EntityFactories;
using Components.Tags;
using Components.Unit;
using Components.Unit.MoveComponents;
using Components.Unit.MoveComponents.Input;
using Components.Unit.Weapon;
using Enums;
using Extensions;
using Extensions.EntityToGameObject;
using Leopotam.Ecs;
using UniRx;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;
        [Inject] private EcsWorld _world;
        
        public override void InstallBindings()
        {
            //Container.Bind<InputActionMap>().FromInstance(IsBlueTeam ? _input.Player : _input.Player1);
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
            _settings.Rigidbody.gameObject.AddComponent<PlayerUnityNotify>();
        }

        private EcsEntity EcsInitPlayer()
        {
            var entity = _world.NewEntity();
            entity.Get<PlayerTag>() = new PlayerTag();
            entity.Get<View>().ViewObject = new ViewObjectUnity(_settings.Rigidbody);
            entity.Get<TeamData>().Team = _settings.Team;
            entity.Get<InputMoveData>();
            entity.Get<InputShootData>();
            ref var move = ref  entity.Get<Move>();
            move.Rotation = _settings.Rigidbody.rotation;
            move.Position = _settings.Rigidbody.position;
            entity.Get<Mass>().Value = _settings.Rigidbody.mass;
            ref var health = ref entity.Get<Health>();
            health.Current = new ReactiveProperty<float>(_settings.MaxHealth);
            ref var energy = ref entity.Get<Energy>();
            energy.InitialEnergy = _settings.MaxEnergy;
            energy.CurrentEnergy = new ReactiveProperty<float>(_settings.MaxEnergy);

            return entity;
        }

        private void SetFirsWeapon(in EcsEntity player, Transform playerTransform,
            IEntityFactory gunEntityFactoryFromSo)
        {
            var gun = gunEntityFactoryFromSo.CreateEntity(_world);
            gun.Get<PlayerOwner>().Owner = player;
            gun.Get<FirstWeapon>();
            gun.Get<ShootIsPossible>();
        }

        private void SetSecondWeapon(in EcsEntity player, Transform playerTransform,
            IEntityFactory gunEntityFactoryFromSo)
        {
            var gun = gunEntityFactoryFromSo.CreateEntity(_world);
            gun.Get<PlayerOwner>().Owner = player;
            gun.Get<FirstWeapon>();
            gun.Get<ShootIsPossible>();
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody2D Rigidbody;
            public AudioSource AudioSource;
            public float MaxHealth;
            public float MaxEnergy;
            public Team Team;
        }
    }
}