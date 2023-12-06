using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Base
{
    public abstract class BaseLateUpdateFeatureProvider : BaseFeatureProvider<LateUpdateFeature>
    {
        public abstract override LateUpdateFeature GetFeature(FeaturesArgs args);
    }
}