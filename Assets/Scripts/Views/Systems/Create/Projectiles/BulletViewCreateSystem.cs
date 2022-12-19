using Model.Components.Extensions.Pool;
using Model.Components.Tags;
using Model.Components.Tags.Projectiles;
using Zenject;

namespace Views.Systems.Create.Projectiles
{
    internal sealed class BulletViewCreateSystem : ProjectileCreateSystem<BulletTag>
    {
        [Inject] private ProjectileView.Factory _factory;
        
        protected override IPhysicsPoolObject GetPoolObject()
        {
            return _factory.Create();
        }
    }
}