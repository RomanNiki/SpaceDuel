using _Project.Develop.Runtime.Core.Effects.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Effects
{
    public class EffectEntityFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new ExplosionSystem());
            AddSystem(new HitEffectSystem());
        }
    }
}