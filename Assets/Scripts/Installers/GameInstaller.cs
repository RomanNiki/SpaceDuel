using System;
using UnityEngine;
using Views;
using Views.Projectiles;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private Settings _settings;

        public override void InstallBindings()
        {        
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindFactory<BulletView, BulletView.Factory>()
                .FromPoolableMemoryPool<BulletView, BulletPool>(poolBinder =>
                    poolBinder.WithInitialSize(20).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings._bulletPrefab).UnderTransformGroup("Bullets"));
            Container.BindFactory<MineView, MineView.Factory>()
                .FromPoolableMemoryPool<MineView, MinePool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings._minePrefab).UnderTransformGroup("Mines"));
            Container.BindFactory<ExplosionView, ExplosionView.Factory>()
                .FromPoolableMemoryPool<ExplosionView, ExplosionPool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings._explosionPrefab).UnderTransformGroup("Explosions"));
        }
        
        private class BulletPool : MonoPoolableMemoryPool<IMemoryPool, BulletView>
        {
        } 
        
        private class MinePool : MonoPoolableMemoryPool<IMemoryPool, MineView>
        {
        } 
        
        private class ExplosionPool : MonoPoolableMemoryPool<IMemoryPool, ExplosionView>
        {
        }

        [Serializable]
        public class Settings
        {
            public GameObject _bulletPrefab;
            public GameObject _minePrefab;
            public GameObject _explosionPrefab;
        }
    }
}