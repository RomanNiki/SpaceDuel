using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Base
{
    public abstract class BaseFeatureProvider<TFeature> : ScriptableObject where TFeature : BaseFeature
    {
        public abstract TFeature GetFeature(IObjectResolver container);
    }
}