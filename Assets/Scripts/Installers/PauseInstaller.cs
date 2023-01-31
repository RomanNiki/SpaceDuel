using Extensions.Pause;
using Zenject;

namespace Installers
{
    public class PauseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PauseService>().AsSingle();
        }
    }
}