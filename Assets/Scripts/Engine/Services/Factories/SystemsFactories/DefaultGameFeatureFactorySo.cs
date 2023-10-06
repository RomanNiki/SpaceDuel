using System;
using System.Collections.Generic;
using Core.Characteristics.Damage;
using Core.Characteristics.EnergyLimits;
using Core.Collisions;
using Core.Effects;
using Core.Init;
using Core.Meta;
using Core.Movement;
using Core.Timers;
using Core.Views;
using Core.Weapon;
using Engine.Input;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace Engine.Services.Factories.SystemsFactories
{
    [CreateAssetMenu(menuName = "SystemsFactory/Default")]
    public sealed class DefaultGameFeatureFactorySo : FeaturesFactoryBaseSo
    {
        public override IEnumerable<UpdateFeature> CreateUpdateFeatures(FeaturesFactoryArgs args)
        {
            return new UpdateFeature[]
            {
                new InitFeature(args.SpawnPoints),
                new InputFeature(),
                new EnergyFeature(),
                new DamageFeature(),
                new EffectEntityFeature(),
                new TimerFeature(),
                new WeaponFeature(),
                new ViewCreateFeature(args.Pools),
                new MetaFeature(args.UIFactory, args.Score, args.Game)
            };
        }
        
        public override IEnumerable<FixedUpdateFeature> CreateFixedUpdateFeatures(FeaturesFactoryArgs args)
        {
            return new FixedUpdateFeature[]
            {
                new MoveFeature(args.MoveLoopService),
                new CollisionsFeature()
            };
        }

        public override IEnumerable<LateUpdateFeature> CreateLateUpdateFeatures(FeaturesFactoryArgs args)
        {
            return Array.Empty<LateUpdateFeature>();
        }
    }
}