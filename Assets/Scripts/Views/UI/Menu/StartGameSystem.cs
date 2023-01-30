using System.Collections.Generic;
using Extensions.AssetLoaders;
using Extensions.Loading.LoadingOperations;
using Leopotam.Ecs;
using Model.Components.Requests;
using Zenject;

namespace Views.UI.Menu
{
    public sealed class StartGameSystem : IEcsRunSystem
    {
        private EcsFilter<StartGameRequest> _filter;
        [Inject] private readonly LoadingScreenProvider _provider;
        [Inject] private readonly GameAssetsLoadProvider _assetsLoadProvider;
        public async void Run()
        {
            if (_filter.IsEmpty())
                return;
            _filter.GetEntity(0).Del<StartGameRequest>();

            
            Queue<ILoadingOperation> loadingOperations = new();
            loadingOperations.Enqueue(new LoadGameAssets(_assetsLoadProvider));
            loadingOperations.Enqueue(new LoadGameLoadingOperation());

            await _provider.LoadAndDestroy(loadingOperations);
        }
    }
}