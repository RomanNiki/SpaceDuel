using Core.Services;
using Engine.Common;
using Engine.Services.AssetLoaders;
using Engine.Services.AssetManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class MenuInstaller : LifetimeScope
    {
        [SerializeField] private AssetPair[] _poolsAssets;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterPools(builder);
            RegisterLoadingAssetLoader(builder);
        }

        private void RegisterPools(IContainerBuilder builder)
        {
            var assetsPools = new AssetsPools(_poolsAssets);
            builder.RegisterInstance(assetsPools).As<IAssets>();
        }

        private static void RegisterLoadingAssetLoader(IContainerBuilder builder)
        {
            builder.Register<LoadingScreenAssetLoader>(Lifetime.Singleton);
        }
    }
}