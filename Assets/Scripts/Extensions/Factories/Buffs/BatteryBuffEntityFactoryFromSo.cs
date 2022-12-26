using System;
using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Components.Tags.Buffs;
using Model.Components.Unit;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extensions.Factories.Buffs
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