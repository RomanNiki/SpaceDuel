using _Project.Develop.Runtime.Core.Buffs;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(BuffFeature))]
    public sealed class BuffFeatureProvider : BaseUpdateFeatureProvider
    {
            public override UpdateFeature GetFeature(FeaturesArgs args) =>
                new BuffFeature(args.Random);
    }
}