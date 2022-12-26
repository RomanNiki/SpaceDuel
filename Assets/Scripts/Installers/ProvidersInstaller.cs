using Extensions.AssetLoaders;
using Zenject;

namespace Installers
{
    public class ProvidersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PrepareGameScreenProvider>().AsSingle();
        }
    }
}