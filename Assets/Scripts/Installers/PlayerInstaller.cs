using System;
using Controller.EntityToGameObject;
using Extensions;
using Extensions.Factories;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Extensions;
using Model.Components.Tags;
using Model.Components.Unit.MoveComponents;
using Model.Components.Unit.MoveComponents.Input;
using Model.Enums;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;
        [SerializeField] private PlayerEntityFactoryFromSo _playerEntityFactory;

        [Inject] private EcsWorld _world;

        public override void InstallBindings()
        {
            var entity = EcsInitPlayer();
            _settings.Rigidbody.transform.GetProvider().SetEntity(entity);
            _settings.Rigidbody.gameObject.AddComponent<PlayerUnityNotify>();
        }

        private EcsEntity EcsInitPlayer()
        {
            var entity = _playerEntityFactory.CreateEntity(_world);
            entity.Get<ViewObjectComponent>().ViewObject = new RigidbodyViewObjectUnity(_settings.Rigidbody);
            entity.Get<UnityComponent<AudioSource>>().Value = _settings.AudioSource;
            entity.Get<UnityComponent<VisualEffect>>().Value = _settings.VisualEffect;
            entity.Get<Team>().Value = _settings.Team;
            entity.Get<ExplosiveTag>();
            entity.AddMove(_settings.Rigidbody.position, _settings.Rigidbody.rotation, _settings.Rigidbody.mass,
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
        }
    }
}