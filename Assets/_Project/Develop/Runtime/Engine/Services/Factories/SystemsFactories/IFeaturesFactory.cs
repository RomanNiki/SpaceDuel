using System.Collections.Generic;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories
{
    public interface IFeaturesFactory
    {
        IEnumerable<LateUpdateFeature> CreateLateUpdateFeatures(FeaturesArgs args);
        IEnumerable<FixedUpdateFeature> CreateFixedUpdateFeatures(FeaturesArgs args);
        IEnumerable<UpdateFeature> CreateUpdateFeatures(FeaturesArgs args);
    }
}