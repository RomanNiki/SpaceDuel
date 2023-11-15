using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Base
{
    public abstract class BaseFixedUpdateFeatureProvider : BaseFeatureProvider<FixedUpdateFeature>
    {
        public abstract override FixedUpdateFeature GetFeature(FeaturesFactoryArgs args);
    }
}