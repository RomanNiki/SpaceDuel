using System.Collections.Generic;
using Extensions.AssetLoaders;
using Extensions.GameStateMachine.Transitions;
using Extensions.Loading.LoadingOperations;
using Leopotam.Ecs;
using Model.Components.Requests;
using Model.Scores.Components;

namespace Extensions.GameStateMachine.States
{
    public class ExitGameState : State
    {
        private readonly EcsFilter<ExitRequest> _filterFilter;
        private readonly EcsFilter<Score> _scoreFilter;
        private readonly LoadingScreenProvider _loadingScreenProvider;
        private readonly GameAssetsLoadProvider _gameAssetsLoadProvider;

        public ExitGameState(EcsFilter<Score> scoreFilter, LoadingScreenProvider provider,
            GameAssetsLoadProvider gameAssetsLoadProvider, List<Transition> transitions)
            : base(transitions)
        {
            _scoreFilter = scoreFilter;
            _loadingScreenProvider = provider;
            _gameAssetsLoadProvider = gameAssetsLoadProvider;
        }

        protected override async void OnEnter()
        {
            foreach (var i in _scoreFilter)
            {
                _scoreFilter.Get1(i).Value = 0;
                _scoreFilter.GetEntity(i).Get<ViewUpdateRequest>();
            }

            var loadingOperations = new Queue<ILoadingOperation>();
            loadingOperations.Enqueue(new LoadMenuLoadingOperation());
            loadingOperations.Enqueue(new UnloadGameAssets(_gameAssetsLoadProvider));
            await _loadingScreenProvider.LoadAndDestroy(loadingOperations);
        }
    }
}