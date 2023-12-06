using System.Collections.Generic;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories
{
    public abstract class BaseFeaturesFactorySo : ScriptableObject, IFeaturesFactory
    {
        public abstract IEnumerable<LateUpdateFeature> CreateLateUpdateFeatures(FeaturesArgs args);

        public abstract IEnumerable<FixedUpdateFeature> CreateFixedUpdateFeatures(FeaturesArgs args);

        public abstract IEnumerable<UpdateFeature> CreateUpdateFeatures(FeaturesArgs args);
    }
}