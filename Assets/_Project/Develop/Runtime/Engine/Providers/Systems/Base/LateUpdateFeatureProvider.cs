using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Unity.VContainer;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Base
{
    public class LateUpdateFeatureProvider<TFeature> : BaseLateUpdateFeatureProvider
        where TFeature : LateUpdateFeature
    {
        public override LateUpdateFeature GetFeature(IObjectResolver container) => container.CreateFeature<TFeature>();
    }
}