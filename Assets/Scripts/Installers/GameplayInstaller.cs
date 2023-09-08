using System;
using Core.Extensions.Pause;
using Core.Extensions.Pause.Services;
using Core.Movement;
using Engine.Extensions;
using Engine.Factories.SystemsFactories;
using Engine.Factories.ViewFactories;
using Engine.Movement.Services;
using Engine.Providers;
using Modules.Pooling.Pool;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private SystemsFactoryBaseSo _systemsFactoryBaseSo;
        [SerializeField] private Camera _orthographicCamera;
        [SerializeField] private Settings _settings;
        
        private void OnValidate()
        {
            if (_orthographicCamera.orthographic == false)
            {
                throw new ArgumentException("Need install orthographic camera");
            }
        }

        public override void InstallBindings()
        {
            BindEcsWorld();
            BindMoveLoopService();
            BindPauseService();
            BindSystemFactory();
            BindSystemArgs();
            BindPools();
        }

        private void BindPools()
        {
            var bulletPool = CreateBulletPool();
            var minePool = CreateMinePool();
            var objectPools = new ObjectPools(bulletPool, minePool);
            Container.Bind<ObjectPools>().FromInstance(objectPools).AsSingle();
        }

        private MonoPool<PoolItemEntityProvider> CreateBulletPool()
        {
            return CreatePool(_settings.BulletReference);
        } 
        
        private MonoPool<PoolItemEntityProvider> CreateMinePool()
        {
            return CreatePool(_settings.MineReference);
        }

        private static MonoPool<PoolItemEntityProvider> CreatePool(AssetReference assetReference, int initialSize = 5)
        {
            var factory = new AddressableViewFactory<PoolItemEntityProvider>(assetReference);
            var settings = new PoolBase<PoolItemEntityProvider>.Settings(int.MaxValue, initialSize);
            var pool = new MonoPool<PoolItemEntityProvider>(settings ,factory);
            return pool;
        }

        private void BindSystemArgs()
        {
            Container.BindInterfacesAndSelfTo<SystemFactoryArgs>().AsSingle();
        }

        private void BindSystemFactory()
        {
            Container.Bind<ISystemFactory>().FromInstance(_systemsFactoryBaseSo).AsSingle();
        }

        private void BindPauseService()
        {
            Container.Bind<IPauseService>().To<PauseService>().AsSingle();
        }

        private void BindMoveLoopService()
        {
            Container.Bind<IMoveLoopService>().To<MoveLoopService>().AsSingle().WithArguments(_orthographicCamera);
        }

        private void BindEcsWorld()
        {
            var world = World.Create();
            Container.Bind<World>().FromInstance(world).AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public AssetReference BulletReference;
            public AssetReference MineReference;
        }
    }
}