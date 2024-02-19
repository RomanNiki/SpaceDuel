using _Project.Develop.Runtime.Core.Init.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Init
{
    public class InitFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddInitializer(new PlayerInitSystem());
        }
    }
}