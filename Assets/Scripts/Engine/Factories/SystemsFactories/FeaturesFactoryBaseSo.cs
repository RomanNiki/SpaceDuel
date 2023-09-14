using System.Collections.Generic;
using Core.Extensions;
using UnityEngine;

namespace Engine.Factories.SystemsFactories
{
    public abstract class FeaturesFactoryBaseSo : ScriptableObject, IFeaturesFactory
    {
        protected readonly List<BaseMorpehFeature> Features = new();
        
        public IEnumerable<BaseMorpehFeature> Create(FeaturesFactoryArgs args)
        {
            Features.Clear();
            Features.AddRange(CreateUpdateFeatures(args));
            Features.AddRange(CreateFixedUpdateFeatures(args));
            Features.AddRange(CreateLateUpdateFeatures(args));
            return Features;
        }

        protected abstract IEnumerable<BaseMorpehFeature> CreateLateUpdateFeatures(FeaturesFactoryArgs args);

        protected abstract IEnumerable<BaseMorpehFeature> CreateFixedUpdateFeatures(FeaturesFactoryArgs args);

        protected abstract IEnumerable<BaseMorpehFeature> CreateUpdateFeatures(FeaturesFactoryArgs args);
    }
}