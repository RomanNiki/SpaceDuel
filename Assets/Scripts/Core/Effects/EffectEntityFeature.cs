using Core.Effects.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Effects
{
    public class EffectEntityFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new ExplosionCreateSystem());
            AddSystem(new HitEffectSystem());
        }
    }
}