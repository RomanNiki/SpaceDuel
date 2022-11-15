using System;
using Models;
using Presenters;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private Settings _settings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindFactory<float, Vector3, Vector3, BulletPresenter, BulletPresenter.Factory>()
                .FromPoolableMemoryPool<float, Vector3, Vector3, BulletPresenter, BulletPool>(poolBinder =>
                    poolBinder.WithInitialSize(20).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings._bulletPrefab).UnderTransformGroup("Bullets"));
            Container.BindFactory<Vector3, MinePresenter, MinePresenter.Factory>()
                .FromPoolableMemoryPool<Vector3, MinePresenter, MinePool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings._minePrefab).UnderTransformGroup("Mines"));
            Container.BindInterfacesAndSelfTo<RestartGameHandler>().AsSingle();
   
            Time.timeScale = 1f;
        }
        
        private class BulletPool : MonoPoolableMemoryPool<float, Vector3, Vector3, IMemoryPool, BulletPresenter>
        {
        }

        private class MinePool : MonoPoolableMemoryPool<Vector3, IMemoryPool, MinePresenter>
        {
        }

        [Serializable]
        public class Settings
        {
            public GameObject _bulletPrefab;
            public GameObject _minePrefab;
        }
    }
}