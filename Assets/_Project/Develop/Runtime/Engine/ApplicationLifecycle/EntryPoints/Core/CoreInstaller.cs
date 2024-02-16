﻿using _Project.Develop.Runtime.Core;
using _Project.Develop.Runtime.Core.Movement;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Factories;
using _Project.Develop.Runtime.Core.Services.Meta;
using _Project.Develop.Runtime.Engine.Services;
using _Project.Develop.Runtime.Engine.Services.Factories.SystemsFactories;
using _Project.Develop.Runtime.Engine.Services.Movement;
using _Project.Develop.Runtime.Engine.UI.Factories;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.ApplicationLifecycle.EntryPoints.Core
{
    public class CoreInstaller : LifetimeScope
    {
        [SerializeField] private BaseFeaturesFactorySo _baseFeaturesFactorySo;
        [SerializeField] private BaseInstaller _featuresInstaller;
        [SerializeField] private Camera _camera;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMoveLoopService(builder);
            RegisterSystemFactory(builder);
            RegisterUIFactory(builder);
            RegisterScore(builder);
            RegisterGame(builder);
            RegisterSystemController(builder);
            builder.RegisterEntryPoint<CoreFlow>();
        }
        
        private static void RegisterGame(IContainerBuilder builder) =>
            builder.Register<IGame, Game>(Lifetime.Singleton);

        private void RegisterSystemController(IContainerBuilder builder) =>
            builder.Register<ISystemsController, SystemsController>(Lifetime.Singleton).WithParameter(_featuresInstaller);
        
        private static void RegisterUIFactory(IContainerBuilder builder) =>
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);

        private void RegisterSystemFactory(IContainerBuilder builder) =>
            builder.RegisterInstance<IFeaturesFactory>(_baseFeaturesFactorySo);

        private static void RegisterScore(IContainerBuilder builder) =>
            builder.Register<IScore, ScoreService>(Lifetime.Singleton);
        
        private void RegisterMoveLoopService(IContainerBuilder builder) =>
            builder.Register<IMoveLoopService, MoveLoopService>(Lifetime.Singleton).WithParameter(_camera);
    }
}