using System.Collections.Generic;
using Extensions.AssetLoaders;
using Extensions.GameStateMachine.States;
using Extensions.GameStateMachine.Transitions;
using Leopotam.Ecs;
using Model.Components.Events;
using Model.Components.Requests;
using Model.Extensions.Pause;
using Model.Scores.Components;
using Model.Unit.Input.Components.Events;
using Zenject;

namespace Controller
{
    public class GameStateSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsWorld _world;
        private readonly IPauseService _pauseService;
        [Inject] private RestartGameState.Settings _restartSettings;
        [Inject] private StartGameState.Settings _startSettings;
        [Inject] private PrepareGameScreenProvider _prepareGameScreenProvider;
        [Inject] private LoadingScreenProvider _loadingScreenProvider;
        [Inject] private PauseMenuProvider _pauseProvider;
        [Inject] private GameAssetsLoadProvider _gameAssetsLoadProvider;
        [Inject] private ControlsScreenProvider _controlsScreenProvider;
        private readonly EcsFilter<PauseRequest> _pauseFilter;
        private readonly EcsFilter<Score> _scoreFilter;
        private readonly EcsFilter<ExitRequest> _exitGameFilter;
        private readonly EcsFilter<RestartGameRequest> _restartRequest;
        private readonly EcsFilter<StartGameRequest> _unpauseFilter;
        private readonly EcsFilter<GameStartedEvent> _gameStartedFilter;
        private readonly EcsFilter<InputShootStartedEvent> _shotStartedEvent;

        private State _currentState;

        public void Run()
        {
            if (_currentState == null)
                return;
            _currentState.Run();
            var nextState = _currentState.GetNextState();
            if (nextState != null)
            {
                Transit(nextState);
            }
        }

        public void Init()
        {
            var startState = InitGameStates();
            _pauseService.SetPaused(true);
            Transit(startState);
        }

        private ShowControlsState InitGameStates()
        {
            var exitState = new ExitGameState(_scoreFilter, _loadingScreenProvider, _gameAssetsLoadProvider,
                new List<Transition>(0));

            var pauseTransitions = new List<Transition>
            {
                new ExitGameTransition(exitState, _exitGameFilter),
            };

            var pauseState = new PauseGameState(_world, _pauseProvider, pauseTransitions);

            var restartState = new RestartGameState(_world, _restartSettings, _pauseService, new List<Transition>());

            var gameProcessTransitions = new List<Transition>
            {
                new RestartTransition(restartState, _restartRequest)
            };

            var gameProcessState = new GameProcessState(_pauseService, gameProcessTransitions);

            var pauseTransition = new PauseTransition(pauseState, _pauseFilter);
            var startGameTransitions = new List<Transition>
            {
                pauseTransition,
                new StartGameProcessTransition(gameProcessState, _gameStartedFilter)
            };

            var startState = new StartGameState(_startSettings, _prepareGameScreenProvider, _world,
                startGameTransitions);

            var unpauseTransition = new UnPauseTransition(startState, _unpauseFilter);
            var anyKeyTransition = new MenuPlayerShotTransition(startState, _shotStartedEvent);

            var controlsState = new ShowControlsState(_controlsScreenProvider,
                new List<Transition>() {pauseTransition, anyKeyTransition});
            pauseTransitions.Add(unpauseTransition);
            gameProcessTransitions.Add(pauseTransition);
            return controlsState;
        }

        private void Transit(State nextState)
        {
            _currentState?.OnExit();
            _currentState = nextState;
            _currentState?.Enter();
        }
    }
}