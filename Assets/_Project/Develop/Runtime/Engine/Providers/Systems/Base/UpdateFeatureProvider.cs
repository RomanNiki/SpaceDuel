using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Base
{
    public class UpdateFeatureProvider<TFeature> : BaseUpdateFeatureProvider
    where TFeature : UpdateFeature, new()
    {
        public override UpdateFeature GetFeature(FeaturesFactoryArgs args) => new TFeature();
    }
}