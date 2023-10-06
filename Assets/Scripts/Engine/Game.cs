using System.Threading.Tasks;
using Core.Services;
using Core.Services.Factories;
using Core.Services.Time;
using Scellecs.Morpeh.Addons.Feature.Unity;

namespace Engine
{
    public class Game : IGame
    {
        private readonly BaseFeaturesInstaller _featuresInstaller;
        private readonly IUIFactory _uiFactory;
        private readonly ITimeScale _timeScale;

        public Game(BaseFeaturesInstaller featuresInstaller, IUIFactory uiFactory, ITimeScale timeScale)
        {
            _featuresInstaller = featuresInstaller;
            _uiFactory = uiFactory;
            _timeScale = timeScale;
        }

        public bool IsPlaying { get; private set; }

        public async Task Start()
        {
            await _uiFactory.OpenControlsWindow(StartInternal);
        }

        public async Task Restart()
        {
            await _timeScale.SlowDown(0.2f);
            IsPlaying = false;
            await Task.Yield();
            _featuresInstaller.gameObject.SetActive(false);
            await _uiFactory.OpenControlsWindow(StartInternal);
            await _timeScale.Accelerate(1, 1f);
        }

        private void StartInternal()
        {
            _uiFactory.CloseControlsWindow();
            _featuresInstaller.gameObject.SetActive(true);
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
            if (_featuresInstaller == null) return;
            _featuresInstaller.gameObject.SetActive(false);
        }
    }
}