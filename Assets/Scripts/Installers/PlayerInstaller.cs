using System;
using Controller.EntityToGameObject;
using Extensions;
using Extensions.Factories;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Extensions;
using Model.Components.Extensions.UI;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents;
using Model.Components.Unit.MoveComponents.Input;
using Model.Components.Weapons;
using Model.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using VisualEffect = UnityEngine.VFX.VisualEffect;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntityFactoryFromSo _playerEntityFactory;
        [SerializeField] private Settings _settings;
        [Inject] private EcsWorld _world;

        public override void InstallBindings()
        {
            var entity = EcsInitPlayer();
            _settings.Rigidbody.transform.GetProvider().SetEntity(entity);
            _settings.Rigidbody.gameObject.AddComponent<EcsUnityNotifier>();
            CreateUI(entity);
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

        private EcsEntity EcsInitPlayer()
        {
            var entity = _playerEntityFactory.CreateEntity(_world);
            entity.Get<ViewObjectComponent>().ViewObject = new ViewObjectUnity(_settings.Rigidbody.transform,_settings.Rigidbody);
            entity.Get<UnityComponent<AudioSource>>().Value = _settings.AudioSource;
            entity.Get<Nozzle>().VisualEffect = _settings.VisualEffect;
            entity.Get<Team>().Value = _settings.Team;
            entity.Get<EnergyBar>().Bar = _settings.EnergyBar;
            entity.Get<HealthBar>().Bar = _settings.HealthBar;
            entity.AddMovementComponents(_settings.Rigidbody.position, _settings.Rigidbody.rotation, _settings.Rigidbody.mass,
                _settings.MoveFriction);
            return entity;
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody2D Rigidbody;
            public AudioSource AudioSource;
            public VisualEffect VisualEffect;
            public float MoveFriction;
            public TeamEnum Team;
            [Header("UI")] public Slider HealthBar;
            public Slider EnergyBar;
            public Transform BarTransform;
            public Vector2 Offset;
        }
    }
}