using _Project.Develop.Runtime.Core.Views;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(ViewCreateFeature))]
    public sealed class ViewCreateFeatureProvider : BaseUpdateFeatureProvider
    {
        public override UpdateFeature GetFeature(FeaturesArgs args) =>
            new ViewCreateFeature(args.Assets);
    }
}