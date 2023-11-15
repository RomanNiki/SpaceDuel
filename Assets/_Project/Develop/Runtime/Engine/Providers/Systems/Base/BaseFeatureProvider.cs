using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Base
{
    public abstract class BaseFeatureProvider<TFeature> : ScriptableObject
    where TFeature : BaseFeature
    {
        public abstract TFeature GetFeature(FeaturesFactoryArgs args);
    }
}