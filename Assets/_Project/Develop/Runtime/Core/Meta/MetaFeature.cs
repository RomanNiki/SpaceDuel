using _Project.Develop.Runtime.Core.Input.Components;
using _Project.Develop.Runtime.Core.Meta.Systems;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Factories;
using _Project.Develop.Runtime.Core.Services.Meta;
using _Project.Develop.Runtime.Core.Services.Pause;
using _Project.Develop.Runtime.Core.Services.Time;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Meta
{
    public class MetaFeature : UpdateFeature
    {
        private readonly IScore _score;
        private readonly IGame _game;
        private readonly IUIFactory _uiFactory;
        private readonly IPauseService _pauseService;
        private readonly ITimeScale _timeScale;

        public MetaFeature(IUIFactory uiFactory, IScore score, IGame game, IPauseService pauseService, ITimeScale timeScale)
        {
            _uiFactory = uiFactory;
            _score = score;
            _game = game;
            _pauseService = pauseService;
            _timeScale = timeScale;
        }
        
        protected override void Initialize()
        {
            RegisterRequest<InputMenuEvent>();
            AddSystem(new PauseSystem(_pauseService, _game));
            AddSystem(new GamePlayHudLifecycleSystem(_uiFactory));
            AddSystem(new PlayerDeathScoreIncreaseSystem(_score));
            AddSystem(new GameOverSystem(_game));
            AddSystem(new PlayersNoEnergySystem(_timeScale));
        }
    }
}