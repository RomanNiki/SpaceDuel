using System;
using Components;
using Extensions;
using Extensions.EntityToGameObject;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SunInstaller : MonoInstaller<SunInstaller>
    {
        [Inject] private readonly EcsWorld _world;
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            var entity = EcsInitSun();
            _settings.Transform.GetProvider().SetEntity(entity);
            _settings.Transform.gameObject.AddComponent<SunUnityNotify>();
        }

        private EcsEntity EcsInitSun()
        {
            var sunEntity = _world.NewEntity();
            ref var sun = ref sunEntity.Get<Sun>();
            sun.Position = _settings.SunPosition;
            sun.GravityForce = _settings.GravityForce;
            return sunEntity;
        }
        
        [Serializable]
        public class Settings
        {
            public Transform Transform;
            public Vector2 SunPosition;
            public float GravityForce;
        }
    }
}