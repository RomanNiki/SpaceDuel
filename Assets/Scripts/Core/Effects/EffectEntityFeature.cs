using Core.Effects.Systems;
using Core.Extensions;
using Cysharp.Threading.Tasks;

namespace Core.Effects
{
    public class EffectEntityFeature : BaseMorpehFeature
    {
        protected async override UniTask InitializeSystems()
        {
            AddSystem(new ExplosionCreateSystem());
            AddSystem(new HitEffectSystem());
        }
    }
}