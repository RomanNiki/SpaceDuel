using System;
using EntityToGameObject;
using Extensions;
using Extensions.MappingUnityToModel;
using Extensions.MappingUnityToModel.Factories;
using Extensions.MappingUnityToModel.Factories.Weapon;
using Leopotam.Ecs;
using Model.Components;
using Model.Enums;
using Model.Extensions;
using Model.Extensions.EntityFactories;
using Model.Unit.Input.Components;
using Model.Weapons.Components;
using UnityEngine;
using UnityEngine.Serialization;
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
        }
        
        private EcsEntity EcsInitPlayer()
        {
            var entity = _playerEntityFactory.CreateEntity(_world);
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(_settings.Rigidbody.transform,_settings.Rigidbody);
            entity.Get<UnityComponent<VisualEffect>>().Value = _settings.VisualEffect;
            entity.Get<UnityComponent<PlayerAudioComponent>>().Value = _settings.PlayerAudioComponent;
            entity.Get<Team>().Value = _settings.Team;
            entity.AddMovementComponents(_settings.Rigidbody.position, _settings.Rigidbody.rotation, _settings.Rigidbody.mass,
                _settings.MoveFriction);
            return entity;
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
            SetWeapon(player, gunEntityFactoryFromSo, WeaponEnum.Primary, _world, _settings.PrimaryWeaponAudioComponent);
        }

        private void SetSecondaryWeapon(in EcsEntity player,
            in IEntityFactory gunEntityFactoryFromSo)
        {
            SetWeapon(player, gunEntityFactoryFromSo, WeaponEnum.Secondary, _world, _settings.PrimaryWeaponAudioComponent);
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody2D Rigidbody;
            [FormerlySerializedAs("PrimaryWeaponAudioUnityComponent")] public GunAudioUnityComponent PrimaryWeaponAudioComponent;
            public VisualEffect VisualEffect;
            public PlayerAudioComponent PlayerAudioComponent;
            public float MoveFriction;
            public TeamEnum Team;
        }
    }
}