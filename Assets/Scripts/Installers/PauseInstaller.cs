using Model.Pause;
using Zenject;

namespace Installers
{
    public class PauseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PauseService>().AsSingle();
        }
    }
}