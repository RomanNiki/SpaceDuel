using _Project.Develop.Runtime.Core.Input;
using _Project.Develop.Runtime.Engine.Providers.Systems.Base;
using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Update
{
    [CreateAssetMenu(menuName = "SpaceDuel/ECS/Systems/Update/" + nameof(InputFeature))]
    public class InputFeatureProvider : BaseUpdateFeatureProvider
    {
        public override UpdateFeature GetFeature(FeaturesFactoryArgs args) => new InputFeature(args.Input);
    }
}