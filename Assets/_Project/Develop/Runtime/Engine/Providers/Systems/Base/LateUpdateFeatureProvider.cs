using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Base
{
    public class LateUpdateFeatureProvider<TFeature> : BaseLateUpdateFeatureProvider
    where TFeature : LateUpdateFeature, new()
    {
        public override LateUpdateFeature GetFeature(FeaturesFactoryArgs args) => new TFeature();
    }
}