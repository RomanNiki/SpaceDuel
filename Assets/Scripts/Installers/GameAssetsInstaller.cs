using Extensions.AssetLoaders;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameAssetsInstaller : MonoInstaller
    {
        [SerializeField] private GameAssetsLoadProvider.Settings _settings;
        
        public override void InstallBindings()
        {
            Container.Bind<GameAssetsLoadProvider.Settings>().FromInstance(_settings).AsSingle()
                .WhenInjectedInto<GameAssetsLoadProvider>();
            Container.BindInterfacesAndSelfTo<GameAssetsLoadProvider>().AsSingle();
        }
    }
}