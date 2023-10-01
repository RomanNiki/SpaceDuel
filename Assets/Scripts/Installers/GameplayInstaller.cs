using System;
using Core.Common;
using Core.Extensions.Pause;
using Core.Extensions.Pause.Services;
using Core.Movement;
using Core.Services;
using Engine.Common;
using Engine.Factories.SystemsFactories;
using Engine.Movement.Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private FeaturesFactoryBaseSo _featuresFactoryBaseSo;
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
            BindPlayerSpawnPoints();
            BindMoveLoopService();
            BindPauseService();
            BindSystemFactory();
            BindSystemArgs();
            BindPools();
        }

        private void BindPlayerSpawnPoints()
        {
            Container.Bind<PlayersSpawnPoints>().FromInstance(_playersSpawnPoints).AsSingle();
        }

        private void BindPools()
        {
            var assetsPools = new AssetsPools(_poolsAssets);
            assetsPools.Load();
            Container.Bind<IAssets>().FromInstance(assetsPools).AsSingle();
        }
        
        private void BindSystemArgs() => Container.BindInterfacesAndSelfTo<FeaturesFactoryArgs>().AsSingle();

        private void BindSystemFactory() =>
            Container.Bind<IFeaturesFactory>().FromInstance(_featuresFactoryBaseSo).AsSingle();

        private void BindPauseService() => Container.Bind<IPauseService>().To<PauseService>().AsSingle();

        private void BindMoveLoopService() =>
            Container.Bind<IMoveLoopService>().To<MoveLoopService>().AsSingle().WithArguments(_orthographicCamera);
    }
}