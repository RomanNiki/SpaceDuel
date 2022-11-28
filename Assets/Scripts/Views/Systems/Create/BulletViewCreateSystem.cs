using Model.Components.Extensions.Pool;
using Model.Components.Tags;
using Views.Projectiles;
using Zenject;

namespace Views.Systems.Create
{
    internal sealed class BulletViewCreateSystem : ProjectileCreateSystem<BulletTag>
    {
        [Inject] private BulletView.Factory _factory;
        
        protected override IPoolObject GetPoolObject()
        {
            return _factory.Create();
        }
    }
}