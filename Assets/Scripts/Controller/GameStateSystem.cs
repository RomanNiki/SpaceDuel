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
        private readonly IPauseService _pauseService;
        private readonly DiContainer _container;
        private readonly EcsFilter<PauseRequest> _pauseFilter;
        private readonly EcsFilter<Score> _scoreFilter;
        private readonly EcsFilter<ExitRequest> _exitGameFilter;
        private readonly EcsFilter<RestartGameRequest> _restartRequest;
        private readonly EcsFilter<StartGameRequest> _unpauseFilter;
        private readonly EcsFilter<GameStartedEvent> _gameStartedFilter;
        private readonly EcsFilter<InputShootStartedEvent> _shotStartedEvent;
        private EcsComponentPool<Score> _component;

        private State _currentState;

        public GameStateSystem(DiContainer container, IPauseService pauseService)
        {
            _container = container;
            _pauseService = pauseService;
        }

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

        private State InitGameStates()
        {
            var exitTransitions = new List<Transition>();
            var exitState = _container.Instantiate<ExitGameState>(new object[] { exitTransitions, _scoreFilter });

            var pauseTransitions = new List<Transition>
            {
                new ExitGameTransition(exitState, _exitGameFilter),
            };

            var pauseState = _container.Instantiate<PauseGameState>(new object[] { pauseTransitions });

            var restartTransitions = new List<Transition>();
            var restartState = _container.Instantiate<RestartGameState>(new object[]
            {
                _pauseService, restartTransitions
            });

            var gameProcessTransitions = new List<Transition>
            {
                new RestartTransition(restartState, _restartRequest)
            };

            var gameProcessState =
                _container.Instantiate<GameProcessState>(new object[] { gameProcessTransitions });

            var pauseTransition = new PauseTransition(pauseState, _pauseFilter);
            var startGameTransitions = new List<Transition>
            {
                pauseTransition,
                new StartGameProcessTransition(gameProcessState, _gameStartedFilter)
            };

            var startState = _container.Instantiate<StartGameState>(new object[] { startGameTransitions });

            var unpauseTransition = new UnPauseTransition(startState, _unpauseFilter);
            var anyKeyTransition = new MenuPlayerShotTransition(startState, _shotStartedEvent);

            var controlTransition = new List<Transition>() { pauseTransition, anyKeyTransition };
            var controlsState = _container.Instantiate<ShowControlsState>(new object[] { controlTransition });
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