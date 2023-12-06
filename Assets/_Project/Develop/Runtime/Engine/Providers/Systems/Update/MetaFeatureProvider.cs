using _Project.Develop.Runtime.Core.Meta;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(MetaFeature))]
    public sealed class MetaFeatureProvider : BaseUpdateFeatureProvider
    {
        public override UpdateFeature GetFeature(FeaturesArgs args) =>
            new MetaFeature(args.UIFactory, args.Score, args.Game, args.PauseService);
    }
}