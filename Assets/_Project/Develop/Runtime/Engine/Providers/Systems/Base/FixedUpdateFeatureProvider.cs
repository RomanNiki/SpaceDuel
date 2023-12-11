using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Unity.VContainer;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Base
{
    public class FixedUpdateFeatureProvider<TFeature> : BaseFixedUpdateFeatureProvider
        where TFeature : FixedUpdateFeature
    {
        public override FixedUpdateFeature GetFeature(IObjectResolver container) => container.CreateFeature<TFeature>();
    }
}