using Extensions.AssetLoaders;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Installers
{
    public class ProvidersInstaller : MonoInstaller
    {
        [SerializeField] private AssetReference _controlsScreenProvider;
        
        public override void InstallBindings()
        {
            Container.Bind<ControlsScreenProvider>().AsSingle().WithArguments(_controlsScreenProvider);
            Container.Bind<PrepareGameScreenProvider>().AsSingle();
            Container.Bind<PauseMenuProvider>().AsSingle();
            Container.Bind<LoadingScreenProvider>().AsSingle();
        }
    }
}