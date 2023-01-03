using System;
using Controller.EntityToGameObject;
using Extensions;
using Extensions.MappingUnityToModel.Factories.Buffs;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Extensions;
using Model.Components.Extensions.EntityFactories;
using Model.Components.Requests;
using Model.Components.Unit;
using Model.Timers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class SunInstaller : MonoInstaller<SunInstaller>
    {
        [Inject] private readonly EcsWorld _world;
        [SerializeField] private Settings _settings;
        [SerializeField] private BuffEntityFactoryFromSo _buffEntityFactory;

        public override void InstallBindings()
        {
            var entity = EcsInitSun();
            _settings.Transform.GetProvider().SetEntity(entity);
            _settings.Transform.gameObject.AddComponent<EcsUnityNotifier>();
        }

        private EcsEntity EcsInitSun()
        {
            var sunEntity = _world.NewEntity();
            ref var sun = ref sunEntity.Get<Sun>();
            sunEntity.AddTransform(_settings.SunPosition);
            sun.GravityForce = _settings.GravityForce;
            sun.OuterRadius = _settings.OuterRadius;
            sun.InnerRadius = _settings.InnerRadius;
            sunEntity.Get<ChargeContainer>().ChargeRequest = new ChargeRequest() {Value = _settings.EnergyChargeAmount};
            sunEntity.Get<EntityFactoryRef<IEntityFactory>>().Value = _buffEntityFactory;
            sunEntity.Get<Timer<TimerBetweenSpawn>>().TimeLeftSec = 10f;
            return sunEntity;
        }
        
        [Serializable]
        public class Settings
        {
            public Transform Transform;
            public Vector2 SunPosition;
            [FormerlySerializedAs("Radius")] public float OuterRadius;
            public float InnerRadius;
            public float GravityForce;
            public float EnergyChargeAmount;
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Vector3.zero, _settings.OuterRadius);
        }
    }
}