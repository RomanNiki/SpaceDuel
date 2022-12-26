using Model.Components.Extensions.Pool;
using Model.Components.Tags.Effects;
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