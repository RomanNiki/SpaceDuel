using System.Collections.Generic;
using Extensions.AssetLoaders;
using Extensions.Loading.LoadingOperations;
using Leopotam.Ecs;
using Model.Components;
using Model.Components.Requests;
using Model.Scores.Components;
using Zenject;

namespace Views.UI.Menu
{
    public sealed class BackMenuSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BackToMenuRequest> _filter;
        private readonly EcsFilter<Score> _score;
        [Inject] private LoadingScreenProvider _provider;
        
        
        public async void Run()
        {
            if (_filter.IsEmpty()) return;
            foreach (var i in _score)
            {
                _score.Get1(i).Value = 0;
                _score.GetEntity(i).Get<ViewUpdateRequest>();
            }
            var loadingOperations = new Queue<ILoadingOperation>();
            loadingOperations.Enqueue(new LoadMenuLoadingOperation());
            await _provider.LoadAndDestroy(loadingOperations);
        }
    }
}