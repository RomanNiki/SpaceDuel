using _Project.Develop.Runtime.Core.Meta.Systems;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Factories;
using _Project.Develop.Runtime.Core.Services.Meta;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Meta
{
    public class MetaFeature : UpdateFeature
    {
        private readonly IScore _score;
        private readonly IGame _game;
        private readonly IUIFactory _uiFactory;

        public MetaFeature(IUIFactory uiFactory, IScore score, IGame game)
        {
            _uiFactory = uiFactory;
            _score = score;
            _game = game;
        }
        
        protected override void Initialize()
        {
            AddSystem(new GamePlayHudLifecycleSystem(_uiFactory));
            AddSystem(new PlayerDeathScoreIncreaseSystem(_score));
            AddSystem(new GameOverSystem(_game));
        }
    }
}