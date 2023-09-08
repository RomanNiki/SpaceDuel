using System.Collections.Generic;
using Core.Extensions;

namespace Engine.Factories.SystemsFactories
{
    public interface ISystemFactory
    {
        IEnumerable<BaseMorpehFeature> CreateUpdateFeatures(SystemFactoryArgs args);
        IEnumerable<BaseMorpehFeature> CreateFixedUpdateFeatures(SystemFactoryArgs args);
        IEnumerable<BaseMorpehFeature> CreateLateUpdateFeatures(SystemFactoryArgs args);
    }
}