using System;
using Controller.EntityToGameObject;
using Extensions;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents;
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
            sunEntity.Get<TransformData>().Position = _settings.SunPosition;
            sun.GravityForce = _settings.GravityForce;
            sun.Radius = _settings.Radius;
            sunEntity.Get<ChargeContainer>().ChargeRequest = new ChargeRequest() {Value = _settings.EnergyChargeAmount};
            return sunEntity;
        }
        
        [Serializable]
        public class Settings
        {
            public Transform Transform;
            public Vector2 SunPosition;
            public float Radius;
            public float GravityForce;
            public float EnergyChargeAmount;
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Vector3.zero, _settings.Radius);
        }
    }
}