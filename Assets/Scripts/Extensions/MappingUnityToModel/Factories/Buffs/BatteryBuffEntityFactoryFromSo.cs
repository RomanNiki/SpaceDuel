using System;
using Leopotam.Ecs;
using Model.Buffs.Components.Tags;
using Model.Components.Requests;
using Model.Unit.EnergySystems.Components;
using Model.Unit.EnergySystems.Components.Requests;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extensions.MappingUnityToModel.Factories.Buffs
{
    [CreateAssetMenu(fileName = "EnergyBuff", menuName = "SpaceDuel/Buffs/EnergyBuff", order = 10)]
    [Serializable]
    public sealed class BatteryBuffEntityFactoryFromSo : BuffEntityFactoryFromSo
    {
        [SerializeField] private float _minEnergy;
        [SerializeField] private float _maxEnergy;

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = base.CreateEntity(world);
            entity.Get<EnergyBuffTag>();
            entity.Get<ChargeContainer>().ChargeRequest = new ChargeRequest()
                {Value = Random.Range(_minEnergy, _maxEnergy)};
            return entity;
        }
    }
}