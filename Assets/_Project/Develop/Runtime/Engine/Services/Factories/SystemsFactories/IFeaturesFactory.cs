using System.Collections.Generic;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories
{
    public interface IFeaturesFactory
    {
        IEnumerable<LateUpdateFeature> CreateLateUpdateFeatures(FeaturesFactoryArgs args);
        IEnumerable<FixedUpdateFeature> CreateFixedUpdateFeatures(FeaturesFactoryArgs args);
        IEnumerable<UpdateFeature> CreateUpdateFeatures(FeaturesFactoryArgs args);
    }
}