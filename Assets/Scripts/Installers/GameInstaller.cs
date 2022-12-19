using System;
using Extensions.Factories;
using Model.Systems.VisualEffects;
using UnityEngine;
using Views;
using Views.Systems.Create.Effects;
using Views.Systems.Create.Projectiles;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private Settings _settings;

        public override void InstallBindings()
        {        
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            
            Container.BindFactory<ProjectileView, ProjectileView.Factory>()
                .FromPoolableMemoryPool<ProjectileView, BulletPool>(poolBinder =>
                    poolBinder.WithInitialSize(20).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings._bulletPrefab).UnderTransformGroup("Bullets")).WhenInjectedInto<BulletViewCreateSystem>();
            Container.BindFactory<ProjectileView, ProjectileView.Factory>()
                .FromPoolableMemoryPool<ProjectileView, MinePool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings._minePrefab).UnderTransformGroup("Mines")).WhenInjectedInto<MineViewCreateSystem>();
            Container.BindFactory<VisualEffectView, VisualEffectView.Factory>()
                .FromPoolableMemoryPool<VisualEffectView, ExplosionPool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings._explosionPrefab).UnderTransformGroup("Explosions")).WhenInjectedInto<ExplosionViewCreateSystem>();     
            Container.BindFactory<VisualEffectView, VisualEffectView.Factory>()
                .FromPoolableMemoryPool<VisualEffectView, HitPool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromSubContainerResolve()
                        .ByNewContextPrefab(_settings._hitPrefab).UnderTransformGroup("HitMarks")).WhenInjectedInto<HitViewCreateSystem>();
            
            Container.BindInterfacesAndSelfTo<VisualEffectEntityFactoryFromSo>().FromInstance(_settings._explosionEntityFactory).AsCached().WhenInjectedInto<ExecuteExplosionSystem>();
            Container.BindInterfacesAndSelfTo<VisualEffectEntityFactoryFromSo>().FromInstance(_settings._hitEntityFactory).AsCached().WhenInjectedInto<ExecuteHitSystem>();
        }
        
        private class BulletPool : MonoPoolableMemoryPool<IMemoryPool, ProjectileView>
        {
        } 
        
        private class MinePool : MonoPoolableMemoryPool<IMemoryPool, ProjectileView>
        {
        } 
        
        private class ExplosionPool : MonoPoolableMemoryPool<IMemoryPool, VisualEffectView>
        {
        }
        
        private class HitPool : MonoPoolableMemoryPool<IMemoryPool, VisualEffectView>
        {
        }

        [Serializable]
        public class Settings
        {
            public GameObject _bulletPrefab;
            public GameObject _minePrefab;
            public GameObject _explosionPrefab;
            public GameObject _hitPrefab;
            public VisualEffectEntityFactoryFromSo _explosionEntityFactory;
            public VisualEffectEntityFactoryFromSo _hitEntityFactory;
        }
    }
}