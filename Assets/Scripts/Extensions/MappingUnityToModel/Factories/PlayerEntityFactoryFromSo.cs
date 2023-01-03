using System;
using Leopotam.Ecs;
using Model.Components.Extensions;
using Model.Components.Extensions.DyingPolicies;
using Model.Components.Tags;
using Model.Components.Tags.Effects;
using Model.Components.Unit;
using Model.Components.Unit.MoveComponents.Input;
using UnityEngine;

namespace Extensions.MappingUnityToModel.Factories
{
    [CreateAssetMenu(fileName = "Player", menuName = "SpaceDuel/Player", order = 10)]
    [Serializable]
    public sealed class PlayerEntityFactoryFromSo : EntityFactoryFromSo
    {
        [SerializeField] private Settings _settings;
       

        public override EcsEntity CreateEntity(EcsWorld world)
        {
            var entity = world.NewEntity();
            entity.Get<PlayerTag>();
            entity.Get<InputMoveData>();
            entity.Get<ExplosiveTag>();
            entity.AddHealth(_settings.MaxHealth, new StandardDyingPolicy())
                .AddEnergy(_settings.MaxEnergy);
            entity.Get<DischargeMoveContainer>().DischargeRequest.Value = _settings.MoveCost;
            entity.Get<DischargeRotateContainer>().DischargeRequest.Value = _settings.RotationCost;
            return entity;
        }

       

        [Serializable]
        public class Settings
        {
            public float MaxHealth;
            public float MaxEnergy;
            public float RotationCost;
            public float MoveCost;
        }
    }
}