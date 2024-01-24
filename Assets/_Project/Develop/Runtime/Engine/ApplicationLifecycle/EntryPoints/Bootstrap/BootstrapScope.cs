using System.Linq;
using _Project.Develop.Modules.Pooling.Core;
using _Project.Develop.Runtime.Core.Input;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Pause;
using _Project.Develop.Runtime.Core.Services.Pause.Services;
using _Project.Develop.Runtime.Core.Services.Random;
using _Project.Develop.Runtime.Core.Services.Time;
using _Project.Develop.Runtime.Engine.Common;
using _Project.Develop.Runtime.Engine.Common.Messages;
using _Project.Develop.Runtime.Engine.Infrastructure.Signals;
using _Project.Develop.Runtime.Engine.Input;
using _Project.Develop.Runtime.Engine.Services.AssetLoaders;
using _Project.Develop.Runtime.Engine.Services.AssetManagement;
using _Project.Develop.Runtime.Engine.Services.Time;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.ApplicationLifecycle.EntryPoints.Bootstrap
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
            RegisterInput(builder);
            RegisterTime(builder);
            RegisterRandom(builder);
            RegisterPauseService(builder);
            RegisterSignals(builder);
            builder.RegisterEntryPoint<BootstrapFlow>();
        }

        private static void RegisterSignals(IContainerBuilder builder)
        {
            builder.RegisterInstance(new MessageChannel<QuitApplicationMessage>()).AsImplementedInterfaces();
        }

        private static void RegisterRandom(IContainerBuilder builder) =>
            builder.RegisterInstance(new FastRandom()).AsImplementedInterfaces();

        private void RegisterPools(IContainerBuilder builder)
        {
           // var assetsPools = new AssetsPools(_poolsAssets);
            builder.Register<AssetsPools>(Lifetime.Singleton).WithParameter(_poolsAssets.AsEnumerable()).As<IAssets>().As<ILoadingResource>();
            //builder.RegisterInstance(assetsPools).As<IAssets>();
        }

        private static void RegisterAssetLoaders(IContainerBuilder builder)
        {
            builder.Register<LoadingScreenAssetLoader>(Lifetime.Singleton);
            builder.Register<ControlsWindowAssetLoader>(Lifetime.Singleton);
            builder.Register<GameplayHudAssetLoader>(Lifetime.Singleton);
            builder.Register<PauseMenuAssetLoader>(Lifetime.Singleton);
            builder.Register<BackgroundAssetLoader>(Lifetime.Singleton);
        }

        private static void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<PlayerInput>(Lifetime.Singleton);
            builder.Register<IInput, KeyboardInput>(Lifetime.Singleton);
        }

        private static void RegisterTime(IContainerBuilder builder)
        {
            var timeScale = new BaseTimeScale();
            builder.RegisterInstance<ITimeScale>(timeScale);
        }

        private static void RegisterPauseService(IContainerBuilder builder) =>
            builder.Register<IPauseService, PauseService>(Lifetime.Singleton);
    }
}