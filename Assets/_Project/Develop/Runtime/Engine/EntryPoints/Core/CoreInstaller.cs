using _Project.Develop.Runtime.Core;
using _Project.Develop.Runtime.Core.Movement;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Factories;
using _Project.Develop.Runtime.Core.Services.Meta;
using _Project.Develop.Runtime.Core.Services.Pause;
using _Project.Develop.Runtime.Core.Services.Pause.Services;
using _Project.Develop.Runtime.Engine.Services;
using _Project.Develop.Runtime.Engine.Services.Factories;
using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using _Project.Develop.Runtime.Engine.Services.Movement;
using Scellecs.Morpeh.Addons.Feature.Unity;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.EntryPoints.Core
{
    public class CoreInstaller : LifetimeScope
    {
        [SerializeField] private BaseFeaturesFactorySo _baseFeaturesFactorySo;
        [SerializeField] private BaseFeaturesInstaller _featuresInstaller;
        [SerializeField] private Camera _camera;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMoveLoopService(builder);
            RegisterPauseService(builder);
            RegisterSystemFactory(builder);
            RegisterSystemArgs(builder);
            RegisterUIFactory(builder);
            RegisterScore(builder);
            RegisterGame(builder);
            RegisterSystemController(builder);
            builder.RegisterEntryPoint<CoreFlow>();
        }
        
        private void RegisterGame(IContainerBuilder builder) =>
            builder.Register<IGame, Game>(Lifetime.Singleton).WithParameter(_featuresInstaller);

        private void RegisterSystemController(IContainerBuilder builder) =>
            builder.Register<ISystemsController, SystemsController>(Lifetime.Singleton).WithParameter(_featuresInstaller);
        
        private static void RegisterUIFactory(IContainerBuilder builder) =>
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
        
        private static void RegisterSystemArgs(IContainerBuilder builder) =>
            builder.Register<FeaturesFactoryArgs>(Lifetime.Singleton);

        private void RegisterSystemFactory(IContainerBuilder builder) =>
            builder.RegisterInstance<IFeaturesFactory>(_baseFeaturesFactorySo);

        private static void RegisterPauseService(IContainerBuilder builder) =>
            builder.Register<IPauseService, PauseService>(Lifetime.Singleton);

        private static void RegisterScore(IContainerBuilder builder) =>
            builder.Register<IScore, ScoreService>(Lifetime.Singleton);
        
        private void RegisterMoveLoopService(IContainerBuilder builder) =>
            builder.Register<IMoveLoopService, MoveLoopService>(Lifetime.Singleton).WithParameter(_camera);
    }
}