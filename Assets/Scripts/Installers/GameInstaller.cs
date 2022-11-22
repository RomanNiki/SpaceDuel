using System;
using Enums;
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
            Container.BindFactory<BulletPresenter, BulletPresenter.Factory>().WithId(BulletsEnum.Bullet)
                .FromPoolableMemoryPool<BulletPresenter, BulletPool>(poolBinder =>
                    poolBinder.WithInitialSize(20).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings._bulletPrefab).UnderTransformGroup("Bullets"));
            Container.BindFactory<BulletPresenter, BulletPresenter.Factory>().WithId(BulletsEnum.Mine)
                .FromPoolableMemoryPool<BulletPresenter, MinePool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings._minePrefab).UnderTransformGroup("Mines"));
            
          //  Container.BindInterfacesAndSelfTo<RestartGameHandler>().AsSingle();
   
            Time.timeScale = 1f;
        }
        
        private class BulletPool : MonoPoolableMemoryPool<IMemoryPool, BulletPresenter>
        {
        } 
        private class MinePool : MonoPoolableMemoryPool<IMemoryPool, BulletPresenter>
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