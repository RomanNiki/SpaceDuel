using System;
using Core.Common;
using Core.Common.Enums;
using Core.Extensions.Pause;
using Core.Extensions.Pause.Services;
using Core.Movement;
using Engine.Common;
using Engine.Factories;
using Engine.Factories.SystemsFactories;
using Engine.Movement.Services;
using Engine.Pools;
using Engine.Providers;
using Modules.Pooling.Core.Pool;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private FeaturesFactoryBaseSo _featuresFactoryBaseSo;
        [SerializeField] private Camera _orthographicCamera;
        [SerializeField] private PoolsAssets _poolsAssets;
        
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
            var objectPools = new AssetsPools();
            objectPools.AddPool(ObjectId.Bullet, bulletPool);
            objectPools.AddPool(ObjectId.Mine, minePool);
            Container.Bind<IAssets>().FromInstance(objectPools).AsSingle();
        }

        private EntityProviderPool CreateBulletPool() => CreatePool(_poolsAssets.BulletReference);

        private EntityProviderPool CreateMinePool() => CreatePool(_poolsAssets.MineReference);

        private static EntityProviderPool CreatePool(AssetReference assetReference, int initialSize = 5)
        {
            var factory = new AddressableViewFactory<PoolableEntityProvider>(assetReference);
            var settings = new PoolBase<PoolableEntityProvider>.Settings(initialSize, int.MaxValue);
            var pool = new EntityProviderPool(settings, factory);
            return pool;
        }

        private void BindSystemArgs() => Container.BindInterfacesAndSelfTo<FeaturesFactoryArgs>().AsSingle();

        private void BindSystemFactory() =>
            Container.Bind<IFeaturesFactory>().FromInstance(_featuresFactoryBaseSo).AsSingle();

        private void BindPauseService() => Container.Bind<IPauseService>().To<PauseService>().AsSingle();

        private void BindMoveLoopService() =>
            Container.Bind<IMoveLoopService>().To<MoveLoopService>().AsSingle().WithArguments(_orthographicCamera);

        private void BindEcsWorld()
        {
            var world = World.Default;
            Container.Bind<World>().FromInstance(world).AsSingle();
        }

        [Serializable]
        public class PoolsAssets
        {
            public AssetReference BulletReference;
            public AssetReference MineReference;
        }
    }
}