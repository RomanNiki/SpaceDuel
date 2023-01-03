using System.Collections.Generic;
using Extensions.AssetLoaders;
using Extensions.Loading.LoadingOperations;
using Leopotam.Ecs;
using Model.Components.Requests;
using Zenject;

namespace Views.UI.Menu
{
    public class BackMenuSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BackToMenuRequest> _filter;
        [Inject] private LoadingScreenProvider _provider;
        
        
        public async void Run()
        {
            if (_filter.IsEmpty()) return;
            var loadingOperations = new Queue<ILoadingOperation>();
            loadingOperations.Enqueue(new LoadMenuLoadingOperation());
            await _provider.LoadAndDestroy(loadingOperations);
        }
    }
}