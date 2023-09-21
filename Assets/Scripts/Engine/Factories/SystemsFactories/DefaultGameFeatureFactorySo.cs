using System;
using System.Collections.Generic;
using Core.Characteristics.Damage;
using Core.Characteristics.EnergyLimits;
using Core.Collisions;
using Core.Effects;
using Core.Extensions;
using Core.Movement;
using Core.Timers;
using Core.Views;
using Core.Weapon;
using Engine.Input;
using UnityEngine;

namespace Engine.Factories.SystemsFactories
{
    [CreateAssetMenu(menuName = "SystemsFactory/Default")]
    public sealed class DefaultGameFeatureFactorySo : FeaturesFactoryBaseSo
    {
        protected override IEnumerable<BaseMorpehFeature> CreateUpdateFeatures(FeaturesFactoryArgs args)
        {
            return new BaseMorpehFeature[]
            {
                new InputFeature(),
                new EnergyFeature(),
                new DamageFeature(),
                new EffectEntityFeature(),
                new TimerFeature(),
                new WeaponFeature(),
                new ViewCreateFeature(args.Pools),
            };
        }
        
        protected override IEnumerable<BaseMorpehFeature> CreateFixedUpdateFeatures(FeaturesFactoryArgs args)
        {
            return new BaseMorpehFeature[]
            {
                new MoveFeature(args.MoveLoopService),
                new CollisionsFeature()
            };
        }

        protected override IEnumerable<BaseMorpehFeature> CreateLateUpdateFeatures(FeaturesFactoryArgs args)
        {
            return Array.Empty<BaseMorpehFeature>();
        }
    }
}