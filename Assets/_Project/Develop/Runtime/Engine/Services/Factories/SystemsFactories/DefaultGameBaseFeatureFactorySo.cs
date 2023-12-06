using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Core.Buffs;
using _Project.Develop.Runtime.Core.Characteristics.Damage;
using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits;
using _Project.Develop.Runtime.Core.Collisions;
using _Project.Develop.Runtime.Core.Effects;
using _Project.Develop.Runtime.Core.Init;
using _Project.Develop.Runtime.Core.Input;
using _Project.Develop.Runtime.Core.Meta;
using _Project.Develop.Runtime.Core.Movement;
using _Project.Develop.Runtime.Core.Timers;
using _Project.Develop.Runtime.Core.Views;
using _Project.Develop.Runtime.Core.Weapon;
using _Project.Develop.Runtime.Engine.Sounds;
using _Project.Develop.Runtime.Engine.UI.Statistics;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories
{
    [CreateAssetMenu(menuName = "SpaceDuel/Factory/Systems/Default")]
    public sealed class DefaultGameBaseFeatureFactorySo : BaseFeaturesFactorySo
    {
        public override IEnumerable<UpdateFeature> CreateUpdateFeatures(FeaturesArgs args)
        {
            return new UpdateFeature[]
            {
                new InitFeature(),
                new InputFeature(args.Input),
                new EnergyFeature(),
                new DamageFeature(),
                new EffectEntityFeature(),
                new SoundFeature(),
                new TimerFeature(),
                new WeaponFeature(),
                new ViewCreateFeature(args.Assets),
                new MetaFeature(args.UIFactory, args.Score, args.Game, args.PauseService),
                new UIStatisticFeature(),
                new BuffFeature(args.Random),
            };
        }

        public override IEnumerable<FixedUpdateFeature> CreateFixedUpdateFeatures(FeaturesArgs args)
        {
            return new FixedUpdateFeature[]
            {
                new MoveFeature(args.MoveLoopService),
                new CollisionsFeature()
            };
        }

        public override IEnumerable<LateUpdateFeature> CreateLateUpdateFeatures(FeaturesArgs args)
        {
            return Array.Empty<LateUpdateFeature>();
        }
    }
}