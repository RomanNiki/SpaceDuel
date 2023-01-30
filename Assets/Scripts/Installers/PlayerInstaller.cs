using System;
using Controller.EntityToGameObject;
using Extensions;
using Extensions.MappingUnityToModel;
using Extensions.MappingUnityToModel.Factories;
using Extensions.MappingUnityToModel.Factories.Weapon;
using Extensions.UI;
using Leopotam.Ecs;
using Model.Components;
using Model.Enums;
using Model.Extensions;
using Model.Extensions.EntityFactories;
using Model.Unit.Input.Components;
using Model.Unit.Movement.Components;
using Model.Weapons.Components;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.VFX;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntityFactoryFromSo _playerEntityFactory;
        [SerializeField] private WeaponEntityFactoryFromSo _primaryWeapon;
        [SerializeField] private WeaponEntityFactoryFromSo _secondaryWeapon;
        [SerializeField] private Settings _settings;
        [Inject] private EcsWorld _world;

        public override void InstallBindings()
        {
            var entity = EcsInitPlayer();
            _settings.Rigidbody.transform.GetProvider().SetEntity(entity);
            _settings.Rigidbody.gameObject.AddComponent<EcsUnityNotifier>();
            SetPrimaryWeapon(entity, _primaryWeapon);
            SetSecondaryWeapon(entity, _secondaryWeapon);
            CreateUI(entity);
        }
        
        private EcsEntity EcsInitPlayer()
        {
            var entity = _playerEntityFactory.CreateEntity(_world);
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(_settings.Rigidbody.transform,_settings.Rigidbody);
            entity.Get<UnityComponent<VisualEffect>>().Value = _settings.VisualEffect;
            entity.Get<UnityComponent<PlayerAudioComponent>>().Value = _settings.PlayerAudioComponent;
            entity.Get<Team>().Value = _settings.Team;
            entity.Get<EnergyBar>().Bar = _settings.EnergyBar;
            entity.Get<HealthBar>().Bar = _settings.HealthBar;
            entity.AddMovementComponents(_settings.Rigidbody.position, _settings.Rigidbody.rotation, _settings.Rigidbody.mass,
                _settings.MoveFriction);
            return entity;
        }

        private void CreateUI(in EcsEntity owner)
        {
            var uiEntity = _world.NewEntity();
            uiEntity.AddTransform(_settings.BarTransform.position, _settings.BarTransform.rotation.z);
            uiEntity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(_settings.BarTransform);
            uiEntity.Get<Follower>().Offset = _settings.Offset;
            uiEntity.Get<PlayerOwner>().Owner = owner;
            _settings.BarTransform.GetProvider().SetEntity(uiEntity);
        }

        private static void SetWeapon(in EcsEntity player,
            IEntityFactory gunEntityFactoryFromSo, WeaponEnum weaponEnum, EcsWorld world, GunAudioUnityComponent audioUnityComponent)
        {
            var gun = gunEntityFactoryFromSo.CreateEntity(world);
            gun.Get<PlayerOwner>().Owner = player;
            gun.Get<WeaponType>() = new WeaponType() {Type = weaponEnum};
            gun.Get<ShootIsPossible>();
            gun.Get<UnityComponent<GunAudioUnityComponent>>().Value = audioUnityComponent;
        }

        private void SetPrimaryWeapon(in EcsEntity player,
            in IEntityFactory gunEntityFactoryFromSo)
        {
            SetWeapon(player, gunEntityFactoryFromSo, WeaponEnum.Primary, _world, _settings.PrimaryWeaponAudioUnityComponent);
        }

        private void SetSecondaryWeapon(in EcsEntity player,
            in IEntityFactory gunEntityFactoryFromSo)
        {
            SetWeapon(player, gunEntityFactoryFromSo, WeaponEnum.Secondary, _world, _settings.SecondaryWeaponAudioUnityComponent);
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody2D Rigidbody;
            public GunAudioUnityComponent PrimaryWeaponAudioUnityComponent;
            public GunAudioUnityComponent SecondaryWeaponAudioUnityComponent;
            public VisualEffect VisualEffect;
            public PlayerAudioComponent PlayerAudioComponent;
            public float MoveFriction;
            public TeamEnum Team;
            [Header("UI")] public Slider HealthBar;
            public Slider EnergyBar;
            public Transform BarTransform;
            public Vector2 Offset;
        }
    }
}