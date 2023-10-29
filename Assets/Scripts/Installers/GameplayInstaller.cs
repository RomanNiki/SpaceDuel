using System;
using Core.Common;
using Core.Input;
using Core.Movement;
using Core.Services;
using Core.Services.Factories;
using Core.Services.Meta;
using Core.Services.Pause;
using Core.Services.Pause.Services;
using Core.Services.Time;
using Engine;
using Engine.Common;
using Engine.Input;
using Engine.Services.AssetLoaders;
using Engine.Services.AssetManagement;
using Engine.Services.Factories;
using Engine.Services.Factories.SystemsFactories;
using Engine.Services.Movement;
using Engine.Services.Time;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Unity;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class GameplayInstaller : LifetimeScope
    {
        [SerializeField] private FeaturesFactoryBaseSo _featuresFactoryBaseSo;
        [SerializeField] private BaseFeaturesInstaller _featuresInstaller;
        [SerializeField] private Camera _orthographicCamera;
        [SerializeField] private AssetPair[] _poolsAssets;
        [SerializeField] private PlayersSpawnPoints _playersSpawnPoints;

        private void OnValidate()
        {
            if (_orthographicCamera.orthographic == false)
            {
                throw new ArgumentException("Need install orthographic camera");
            }
        }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterPools(builder);
            RegisterInput(builder);
            RegisterPlayerSpawnPoints(builder);
            RegisterMoveLoopService(builder);
            RegisterPauseService(builder);
            RegisterSystemFactory(builder);
            RegisterSystemArgs(builder);
            RegisterAssetLoaders(builder);
            RegisterUIFactory(builder);
            RegisterScore(builder);
            RegisterGame(builder);
            RegisterTime(builder);
        }

        private static void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<PlayerInput>(Lifetime.Singleton);
            builder.Register<IInput, KeyboardInput>(Lifetime.Singleton);
        }

        private static void RegisterTime(IContainerBuilder builder) =>
            builder.Register<ITimeScale, BaseTimeScale>(Lifetime.Singleton);

        private static void RegisterScore(IContainerBuilder builder) =>
            builder.Register<IScore, ScoreService>(Lifetime.Singleton);

        private void RegisterGame(IContainerBuilder builder) =>
            builder.Register<IGame, Game>(Lifetime.Singleton).WithParameter(_featuresInstaller);

        private static void RegisterUIFactory(IContainerBuilder builder) =>
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);

        private static void RegisterAssetLoaders(IContainerBuilder builder)
        {
            builder.Register<ControlsWindowAssetLoader>(Lifetime.Singleton);
            builder.Register<GameplayHudAssetLoader>(Lifetime.Singleton);
        }

        private void RegisterPlayerSpawnPoints(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playersSpawnPoints).AsSelf();
        }

        private void RegisterPools(IContainerBuilder builder)
        {
            var assetsPools = new AssetsPools(_poolsAssets);
            builder.RegisterInstance(assetsPools).As<IAssets>();
        }

        private static void RegisterSystemArgs(IContainerBuilder builder) =>
            builder.Register<FeaturesFactoryArgs>(Lifetime.Singleton);

        private void RegisterSystemFactory(IContainerBuilder builder) =>
            builder.RegisterInstance<IFeaturesFactory>(_featuresFactoryBaseSo);

        private static void RegisterPauseService(IContainerBuilder builder) =>
            builder.Register<IPauseService, PauseService>(Lifetime.Singleton);

        private void RegisterMoveLoopService(IContainerBuilder builder) =>
            builder.Register<IMoveLoopService, MoveLoopService>(Lifetime.Singleton).WithParameter(_orthographicCamera);
    }
}