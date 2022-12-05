using Model.Components.Extensions.Pool;
using Model.Components.Tags;
using Views.Projectiles;
using Zenject;

namespace Views.Systems.Create
{
    internal sealed class MineViewCreateSystem : ProjectileCreateSystem<MineTag>
    {
        [Inject] private MineView.Factory _factory;
        
        protected override IPhysicsPoolObject GetPoolObject()
        {
            return _factory.Create();
        }
    }
}