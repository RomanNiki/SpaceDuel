using System.Collections.Generic;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories
{
    public abstract class BaseFeaturesFactorySo : ScriptableObject, IFeaturesFactory
    {
        public abstract IEnumerable<LateUpdateFeature> CreateLateUpdateFeatures(IObjectResolver container);

        public abstract IEnumerable<FixedUpdateFeature> CreateFixedUpdateFeatures(IObjectResolver container);

        public abstract IEnumerable<UpdateFeature> CreateUpdateFeatures(IObjectResolver container);
    }
}