using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Base
{
    public class FixedUpdateFeatureProvider<TFeature> : BaseFixedUpdateFeatureProvider
        where TFeature : FixedUpdateFeature, new()
    {
        public override FixedUpdateFeature GetFeature(FeaturesFactoryArgs args) => new TFeature();
    }
}