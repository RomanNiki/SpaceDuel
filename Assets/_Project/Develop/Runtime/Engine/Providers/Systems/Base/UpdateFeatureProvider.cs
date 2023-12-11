using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Unity.VContainer;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Providers.Systems.Base
{
    public class UpdateFeatureProvider<TFeature> : BaseUpdateFeatureProvider
        where TFeature : UpdateFeature
    {
        public override UpdateFeature GetFeature(IObjectResolver container) => container.CreateFeature<TFeature>();
    }
}