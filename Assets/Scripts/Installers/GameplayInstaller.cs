using System;
using Core.Common;
using Core.Movement;
using Core.Services;
using Core.Services.Factories;
using Core.Services.Meta;
using Core.Services.Pause;
using Core.Services.Pause.Services;
using Core.Services.Time;
using Engine;
using Engine.Common;
using Engine.Services.AssetLoaders;
using Engine.Services.AssetManagement;
using Engine.Services.Factories;
using Engine.Services.Factories.SystemsFactories;
using Engine.Services.Movement;
using Engine.Services.Time;
using Modules.Pooling.Core;
using Scellecs.Morpeh.Addons.Feature.Unity;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
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

        public override void InstallBindings()
        {
            BindPools();
            BindPlayerSpawnPoints();
            BindMoveLoopService();
            BindPauseService();
            BindSystemFactory();
            BindSystemArgs();
            BindAssetLoaders();
            BindUIFactory();
            BindScore();
            BindGame();
            BindTime();
        }

        private void BindTime() => Container.Bind<ITimeScale>().To<BaseTimeScale>().AsSingle();

        private void BindScore() => Container.Bind<IScore>().To<ScoreService>().AsSingle();

        private void BindGame() => Container.Bind<IGame>().To<Game>().AsSingle().WithArguments(_featuresInstaller);

        private void BindUIFactory() => Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();

        private void BindAssetLoaders()
        {
            Container.Bind<ControlsWindowAssetLoader>().AsSingle();
            Container.Bind<GameplayHudAssetLoader>().AsSingle();
        }

        private void BindPlayerSpawnPoints() =>
            Container.Bind<PlayersSpawnPoints>().FromInstance(_playersSpawnPoints).AsSingle();

        private void BindPools()
        {
            var assetsPools = new AssetsPools(_poolsAssets);
            Container.Bind(typeof(IAssets), typeof(ILoadingResource)).FromInstance(assetsPools).AsSingle();
        }

        private void BindSystemArgs() => Container.BindInterfacesAndSelfTo<FeaturesFactoryArgs>().AsSingle();

        private void BindSystemFactory() =>
            Container.Bind<IFeaturesFactory>().FromInstance(_featuresFactoryBaseSo).AsSingle();

        private void BindPauseService() => Container.Bind<IPauseService>().To<PauseService>().AsSingle();

        private void BindMoveLoopService() =>
            Container.Bind<IMoveLoopService>().To<MoveLoopService>().AsSingle().WithArguments(_orthographicCamera);
    }
}