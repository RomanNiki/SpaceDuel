using System;
using System.Collections.Generic;
using Core.Characteristics.Damage;
using Core.Characteristics.EnergyLimits;
using Core.Collisions;
using Core.Extensions;
using Core.Movement;
using Core.Timers;
using Core.Weapon;
using Engine.Input;
using Engine.Views.Systems;
using UnityEngine;

namespace Engine.Factories.SystemsFactories
{
    [CreateAssetMenu(menuName = "SystemsFactory/Default")]
    public sealed class DefaultGameSystemFactorySo : SystemsFactoryBaseSo
    {
        public override IEnumerable<BaseMorpehFeature> CreateUpdateFeatures(SystemFactoryArgs args)
        {
            return new BaseMorpehFeature[]
            {
                new InputFeature(),
                new EnergyFeature(),
                new DamageFeature(),
                new TimerFeature(),
                new WeaponFeature(),
                new ViewCreateFeature(args.Pools)
            };
        }

        public override IEnumerable<BaseMorpehFeature> CreateFixedUpdateFeatures(SystemFactoryArgs args)
        {
            return new BaseMorpehFeature[]
            {
                new MoveFeature(args.MoveLoopService),
                new CollisionsFeature()
            };
        }

        public override IEnumerable<BaseMorpehFeature> CreateLateUpdateFeatures(SystemFactoryArgs args)
        {
            return Array.Empty<BaseMorpehFeature>();
        }
    }
}