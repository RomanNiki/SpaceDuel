using Core.Extensions;
using Core.Extensions.Clear.Systems;
using Core.Views.Components;
using Core.Weapon.Components;
using Cysharp.Threading.Tasks;
using Engine.Extensions;
using Engine.Views.Systems.Create;

namespace Engine.Views.Systems
{
    public class ViewCreateFeature : BaseMorpehFeature
    {
        private readonly ObjectPools _pools;

        public ViewCreateFeature(ObjectPools pools) => _pools = pools;
        
        protected override async UniTask InitializeSystems()
        {
            await _pools.Load();
            AddSystem(new DellHereUpdateSystem<ViewCreatedEvent>());
            AddSystem(new ViewCreateSystem<BulletTag>(_pools.BulletFactory));
            AddSystem(new ViewCreateSystem<MineTag>(_pools.MineFactory));
        }

        protected override void OnDispose() => _pools.Dispose();
    }
}