using Core.Common;
using Core.Extensions;
using Core.Extensions.Clear.Systems;
using Core.Views.Components;
using Core.Views.Systems.Create;
using Cysharp.Threading.Tasks;

namespace Core.Views
{
    public class ViewCreateFeature : BaseMorpehFeature
    {
        private readonly IAssets _pools;

        public ViewCreateFeature(IAssets pools) => _pools = pools;
        
        protected override async UniTask InitializeSystems()
        {
            await _pools.Load();
            AddSystem(new DellHereUpdateSystem<SpawnedEvent>());
            AddSystem(new SpawnSystem(_pools));
        }

        protected override void OnDispose() => _pools.Dispose();
    }
}