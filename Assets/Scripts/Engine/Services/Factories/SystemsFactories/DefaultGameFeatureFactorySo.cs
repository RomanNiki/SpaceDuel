using System;
using System.Collections.Generic;
using Core.Buffs;
using Core.Characteristics.Damage;
using Core.Characteristics.EnergyLimits;
using Core.Collisions;
using Core.Effects;
using Core.Init;
using Core.Input;
using Core.Meta;
using Core.Movement;
using Core.Timers;
using Core.Views;
using Core.Weapon;
using Engine.Sounds;
using Engine.UI.Statistics;
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
                new InputFeature(args.Input),
                new EnergyFeature(),
                new DamageFeature(),
                new EffectEntityFeature(),
                new SoundFeature(),
                new TimerFeature(),
                new WeaponFeature(),
                new ViewCreateFeature(args.Assets),
                new MetaFeature(args.UIFactory, args.Score, args.Game),
                new UIStatisticFeature(),
                new BuffFeature(),
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