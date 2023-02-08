using Extensions;
using Zenject;

namespace Installers
{
    public class StartupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SystemRegisterHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Startup>().AsSingle().NonLazy();
        }
    }
}