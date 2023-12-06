using _Project.Develop.Runtime.Core.Input.Components;
using _Project.Develop.Runtime.Core.Meta.Systems;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Factories;
using _Project.Develop.Runtime.Core.Services.Meta;
using _Project.Develop.Runtime.Core.Services.Pause;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Meta
{
    public class MetaFeature : UpdateFeature
    {
        private readonly IScore _score;
        private readonly IGame _game;
        private readonly IUIFactory _uiFactory;
        private readonly IPauseService _pauseService;

        public MetaFeature(IUIFactory uiFactory, IScore score, IGame game, IPauseService pauseService)
        {
            _uiFactory = uiFactory;
            _score = score;
            _game = game;
            _pauseService = pauseService;
        }
        
        protected override void Initialize()
        {
            RegisterRequest<InputMenuEvent>();
            AddSystem(new PauseSystem(_pauseService, _game));
            AddSystem(new GamePlayHudLifecycleSystem(_uiFactory));
            AddSystem(new PlayerDeathScoreIncreaseSystem(_score));
            AddSystem(new GameOverSystem(_game));
        }
    }
}