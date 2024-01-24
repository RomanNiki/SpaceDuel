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
using _Project.Develop.Runtime.Engine.ECS.Sounds;
using _Project.Develop.Runtime.Engine.ECS.UI.PlayerUI;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Unity.VContainer;
using UnityEngine;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories
{
    [CreateAssetMenu(menuName = "SpaceDuel/Factory/Systems/Default")]
    public sealed class DefaultGameBaseFeatureFactorySo : BaseFeaturesFactorySo
    {
        public override IEnumerable<UpdateFeature> CreateUpdateFeatures(IObjectResolver container)
        {
            return new UpdateFeature[]
            {
                container.CreateFeature<InitFeature>(),
                container.CreateFeature<InputFeature>(),
                container.CreateFeature<EnergyFeature>(),
                container.CreateFeature<DamageFeature>(),
                container.CreateFeature<EffectEntityFeature>(),
                container.CreateFeature<SoundFeature>(),
                container.CreateFeature<TimerFeature>(),
                container.CreateFeature<WeaponFeature>(),
                container.CreateFeature<ViewCreateFeature>(),
                container.CreateFeature<MetaFeature>(),
                container.CreateFeature<UIStatisticFeature>(),
                container.CreateFeature<BuffFeature>(),
            };
        }

        public override IEnumerable<FixedUpdateFeature> CreateFixedUpdateFeatures(IObjectResolver container)
        {
            return new FixedUpdateFeature[]
            {
                container.CreateFeature<MoveFeature>(),
                container.CreateFeature<CollisionsFeature>(),
            };
        }

        public override IEnumerable<LateUpdateFeature> CreateLateUpdateFeatures(IObjectResolver container)
        {
            return Array.Empty<LateUpdateFeature>();
        }
    }
}