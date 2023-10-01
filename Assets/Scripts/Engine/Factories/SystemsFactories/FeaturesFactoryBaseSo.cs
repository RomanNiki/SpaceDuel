using System.Collections.Generic;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace Engine.Factories.SystemsFactories
{
    public abstract class FeaturesFactoryBaseSo : ScriptableObject, IFeaturesFactory
    {
        public abstract IEnumerable<LateUpdateFeature> CreateLateUpdateFeatures(FeaturesFactoryArgs args);

        public abstract IEnumerable<FixedUpdateFeature> CreateFixedUpdateFeatures(FeaturesFactoryArgs args);

        public abstract IEnumerable<UpdateFeature> CreateUpdateFeatures(FeaturesFactoryArgs args);
    }
}