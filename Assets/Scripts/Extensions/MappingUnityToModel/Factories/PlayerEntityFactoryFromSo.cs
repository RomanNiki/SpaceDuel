using System;
using Leopotam.Ecs;
using Model.Extensions;
using Model.Extensions.DyingPolicies;
using Model.Unit.EnergySystems.Components;
using Model.Unit.Input.Components;
using Model.Unit.Movement.Components;
using Model.Unit.Movement.Components.Tags;
using Model.VisualEffects.Components.Tags;
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
            entity.Get<Nozzle>();
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