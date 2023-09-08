using System.Collections.Generic;
using Core.Extensions;
using UnityEngine;

namespace Engine.Factories.SystemsFactories
{
    public abstract class SystemsFactoryBaseSo : ScriptableObject, ISystemFactory
    {
        public abstract IEnumerable<BaseMorpehFeature> CreateUpdateFeatures(SystemFactoryArgs args);
        public abstract IEnumerable<BaseMorpehFeature> CreateFixedUpdateFeatures(SystemFactoryArgs args);
        public abstract IEnumerable<BaseMorpehFeature> CreateLateUpdateFeatures(SystemFactoryArgs args);
    }
}