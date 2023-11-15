using System.Collections.Generic;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories
{
    public abstract class BaseFeaturesFactorySo : ScriptableObject, IFeaturesFactory
    {
        public abstract IEnumerable<LateUpdateFeature> CreateLateUpdateFeatures(FeaturesFactoryArgs args);

        public abstract IEnumerable<FixedUpdateFeature> CreateFixedUpdateFeatures(FeaturesFactoryArgs args);

        public abstract IEnumerable<UpdateFeature> CreateUpdateFeatures(FeaturesFactoryArgs args);
    }
}