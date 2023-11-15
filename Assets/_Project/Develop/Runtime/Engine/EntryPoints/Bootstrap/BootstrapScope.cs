using _Project.Develop.Runtime.Core.Input;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Time;
using _Project.Develop.Runtime.Engine.Common;
using _Project.Develop.Runtime.Engine.Input;
using _Project.Develop.Runtime.Engine.Services.AssetLoaders;
using _Project.Develop.Runtime.Engine.Services.AssetManagement;
using _Project.Develop.Runtime.Engine.Services.Time;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.EntryPoints.Bootstrap
{
    public class BootstrapScope : LifetimeScope
    {
        [SerializeField] private AssetPair[] _poolsAssets;

        protected override void Awake()
        {
            IsRoot = true;
            DontDestroyOnLoad(this);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterPools(builder);
            RegisterAssetLoaders(builder);
            RegisterBackgroundAssetLoader(builder);
            RegisterInput(builder);
            RegisterTime(builder);
            builder.RegisterEntryPoint<BootstrapFlow>();
        }

        private static void RegisterBackgroundAssetLoader(IContainerBuilder builder)
        {
            builder.Register<BackgroundAssetLoader>(Lifetime.Singleton);
        }

        private void RegisterPools(IContainerBuilder builder)
        {
            var assetsPools = new AssetsPools(_poolsAssets);
            builder.RegisterInstance(assetsPools).As<IAssets>();
        }

        private static void RegisterAssetLoaders(IContainerBuilder builder)
        {
            builder.Register<LoadingScreenAssetLoader>(Lifetime.Singleton);
            builder.Register<ControlsWindowAssetLoader>(Lifetime.Singleton);
            builder.Register<GameplayHudAssetLoader>(Lifetime.Singleton);
        }
        
        private static void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<PlayerInput>(Lifetime.Singleton);
            builder.Register<IInput, KeyboardInput>(Lifetime.Singleton);
        }

        private static void RegisterTime(IContainerBuilder builder) =>
            builder.Register<ITimeScale, BaseTimeScale>(Lifetime.Singleton);
    }
}