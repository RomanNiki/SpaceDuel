using System.Collections.Generic;
using Scellecs.Morpeh.Addons.Feature;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories
{
    public interface IFeaturesFactory
    {
        IEnumerable<LateUpdateFeature> CreateLateUpdateFeatures(IObjectResolver container);
        IEnumerable<FixedUpdateFeature> CreateFixedUpdateFeatures(IObjectResolver container);
        IEnumerable<UpdateFeature> CreateUpdateFeatures(IObjectResolver container);
    }
}