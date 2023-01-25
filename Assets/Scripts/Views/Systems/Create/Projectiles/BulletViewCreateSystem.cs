using Model.Weapons.Components.Tags;
using Views.Extensions.Pools;
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