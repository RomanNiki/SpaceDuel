using Model.Components.Extensions.Pool;
using Model.Components.Tags.Effects;
using Zenject;

namespace Views.Systems.Create.Effects
{
    public sealed class ExplosionViewCreateSystem : VisualEffectViewCreateSystem<ExplosionTag>
    {
        [Inject] private VisualEffectView.Factory _factory;
        
        protected override IVisualEffectPoolObject GetPoolObject()
        {
            return _factory.Create();
        }
    }
} 