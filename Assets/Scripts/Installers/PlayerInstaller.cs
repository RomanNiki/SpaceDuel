using System;
using Controller.EntityToGameObject;
using Extensions;
using Extensions.Factories;
using Leopotam.Ecs;
using Model.Components.Extensions.EntityFactories;
using Model.Components.Tags;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents;
using Model.Components.Unit.MoveComponents.Input;
using Model.Components.Weapons;
using Model.Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;
        [SerializeField] private WeaponEntityFactoryFromSo _primaryWeapon;
        [SerializeField] private WeaponEntityFactoryFromSo _secondaryWeapon;

        [Inject] private EcsWorld _world;

        public override void InstallBindings()
        {
            var entity = EcsInitPlayer();
            _settings.Rigidbody.transform.GetProvider().SetEntity(entity);
            _settings.Rigidbody.gameObject.AddComponent<PlayerUnityNotify>();
            SetPrimaryWeapon(entity, _primaryWeapon);
            SetSecondaryWeapon(entity, _secondaryWeapon);
        }

        private EcsEntity EcsInitPlayer()
        {
            var entity = _world.NewEntity();
            entity.Get<PlayerTag>();
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(_settings.Rigidbody);
            entity.Get<Team>().Value = _settings._teamEnum;
            entity.Get<InputMoveData>();

            ref var transformData = ref entity.Get<TransformData>();
            transformData.Rotation = _settings.Rigidbody.rotation;
            transformData.Position = _settings.Rigidbody.position;
            entity.Get<Move>();
            entity.Get<Mass>().Value = _settings.Rigidbody.mass;
            entity.Get<Friction>().Value = _settings.MoveFriction;
            ref var health = ref entity.Get<Health>();
            health.Current = _settings.MaxHealth;
            ref var energy = ref entity.Get<Energy>();
            energy.InitialEnergy = _settings.MaxEnergy;
            energy.CurrentEnergy = _settings.MaxEnergy;
            entity.Get<DischargeMoveContainer>().DischargeRequest.Value = _settings.MoveCost;
            entity.Get<DischargeRotateContainer>().DischargeRequest.Value = _settings.RotationCost;
            return entity;
        }

        private EcsEntity SetWeapon(in EcsEntity player,
            IEntityFactory gunEntityFactoryFromSo, WeaponEnum weaponEnum)
        {
            var gun = gunEntityFactoryFromSo.CreateEntity(_world);
            gun.Get<PlayerOwner>().Owner = player;
            gun.Get<WeaponType>()  = new WeaponType(){ Type = weaponEnum};
            gun.Get<ShootIsPossible>();
            return gun;
        }

        private void SetPrimaryWeapon(in EcsEntity player,
            in IEntityFactory gunEntityFactoryFromSo)
        {
           var gun = SetWeapon(player, gunEntityFactoryFromSo, WeaponEnum.Primary);
        }

        private void SetSecondaryWeapon(in EcsEntity player,
            in IEntityFactory gunEntityFactoryFromSo)
        {
            var gun =  SetWeapon(player, gunEntityFactoryFromSo, WeaponEnum.Secondary);
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody2D Rigidbody;
            public AudioSource AudioSource;
            public float MaxHealth;
            public float MaxEnergy;
            public float RotationCost;
            public float MoveCost;
            public float MoveFriction;
            [FormerlySerializedAs("Team")] public TeamEnum _teamEnum;
        }
    }
}