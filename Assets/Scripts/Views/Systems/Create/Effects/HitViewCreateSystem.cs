using Model.VisualEffects.Components.Tags;
using Views.Extensions.Pools;
using Zenject;

namespace Views.Systems.Create.Effects
{
    public class HitViewCreateSystem : VisualEffectViewCreateSystem<HitTag>
    {
        [Inject] private VisualEffectView.Factory _factory;
        
        protected override IVisualEffectPoolObject GetPoolObject()
        {
            return _factory.Create();
        }
    }
}