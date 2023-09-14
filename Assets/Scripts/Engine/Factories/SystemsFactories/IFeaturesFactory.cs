using System.Collections.Generic;
using Core.Extensions;

namespace Engine.Factories.SystemsFactories
{
    public interface IFeaturesFactory
    {
        IEnumerable<BaseMorpehFeature> Create(FeaturesFactoryArgs args);
    }
}