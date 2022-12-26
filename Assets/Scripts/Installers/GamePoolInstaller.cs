using System;
using Extensions.Factories;
using Model.Systems.VisualEffects;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Views;
using Views.Systems.Create.Buffs;
using Views.Systems.Create.Effects;
using Views.Systems.Create.Projectiles;
using Zenject;

namespace Installers
{
    public class GamePoolInstaller : MonoInstaller
    {
        [Inject] private Settings _settings;

        public override void InstallBindings()
        {
            var bulletFactory = new AddressableFactory<ProjectileView>(Container,_settings._bulletAssetReference);
            var mineFactory = new AddressableFactory<ProjectileView>(Container,_settings._mineAssetReference);
            var hitFactory = new AddressableFactory<VisualEffectView>(Container,_settings._hitAssetReference);
            var explosionFactory = new AddressableFactory<VisualEffectView>(Container,_settings._explosionAssetReference);
            var energyFactory = new AddressableFactory<GameObjectView>(Container,_settings._energyAssetReference);
            Container.BindInterfacesTo<AddressableFactory<ProjectileView>>().FromInstance( bulletFactory).WithArguments(_settings._bulletAssetReference);
            Container.BindInterfacesTo<AddressableFactory<ProjectileView>>().FromInstance(mineFactory).WithArguments(_settings._mineAssetReference);
            Container.BindInterfacesTo<AddressableFactory<VisualEffectView>>().FromInstance(explosionFactory).WithArguments(_settings._explosionAssetReference);
            Container.BindInterfacesTo<AddressableFactory<VisualEffectView>>().FromInstance(hitFactory).WithArguments(_settings._hitAssetReference);
            Container.BindInterfacesTo<AddressableFactory<GameObjectView>>().FromInstance(energyFactory).WithArguments(_settings._energyAssetReference);
            Container.BindFactory<ProjectileView, ProjectileView.Factory>()
                .FromPoolableMemoryPool<ProjectileView, BulletPool>(poolBinder =>
                    poolBinder.WithInitialSize(20).FromIFactory(x =>
                    {
                        x.To<AddressableFactory<ProjectileView>>().FromInstance(bulletFactory).AsCached()
                            .WithArguments(_settings._bulletAssetReference).NonLazy();
                    }).Lazy())
                .WhenInjectedInto<BulletViewCreateSystem>().Lazy();
            Container.BindFactory<ProjectileView, ProjectileView.Factory>()
                .FromPoolableMemoryPool<ProjectileView, MinePool>(poolBinder =>
                    poolBinder.WithInitialSize(3).FromIFactory(x =>
                    {
                        x.To<AddressableFactory<ProjectileView>>().FromInstance(mineFactory).AsCached()
                            .WithArguments(_settings._mineAssetReference).NonLazy();
                    }).Lazy())
                .WhenInjectedInto<MineViewCreateSystem>().Lazy(); 
            Container.BindFactory<VisualEffectView, VisualEffectView.Factory>()
                .FromPoolableMemoryPool<VisualEffectView, HitPool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromIFactory(x =>
                    {
                        x.To<AddressableFactory<VisualEffectView>>().FromInstance(hitFactory).AsCached()
                            .WithArguments(_settings._hitAssetReference).NonLazy();
                    }).Lazy())
                .WhenInjectedInto<HitViewCreateSystem>().Lazy(); 
            Container.BindFactory<VisualEffectView, VisualEffectView.Factory>()
                .FromPoolableMemoryPool<VisualEffectView, ExplosionPool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromIFactory(x =>
                    {
                        x.To<AddressableFactory<VisualEffectView>>().FromInstance(explosionFactory).AsCached()
                            .WithArguments(_settings._explosionAssetReference).NonLazy();
                    }).Lazy())
                .WhenInjectedInto<ExplosionViewCreateSystem>().Lazy(); 
            Container.BindFactory<GameObjectView, GameObjectView.Factory>()
                .FromPoolableMemoryPool<GameObjectView, EnergyBuffPool>(poolBinder =>
                    poolBinder.WithInitialSize(5).FromIFactory(x =>
                    {
                        x.To<AddressableFactory<GameObjectView>>().FromInstance(energyFactory).AsCached()
                            .WithArguments(_settings._energyAssetReference).NonLazy();
                    }).Lazy())
                .WhenInjectedInto<EnergyBuffViewCreateSystem>().Lazy();

            Container.BindInterfacesAndSelfTo<VisualEffectEntityFactoryFromSo>()
                .FromInstance(_settings._explosionEntityFactory).AsCached().WhenInjectedInto<ExecuteExplosionSystem>();
            Container.BindInterfacesAndSelfTo<VisualEffectEntityFactoryFromSo>()
                .FromInstance(_settings._hitEntityFactory).AsCached().WhenInjectedInto<ExecuteHitSystem>();
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

        private class EnergyBuffPool : MonoPoolableMemoryPool<IMemoryPool, GameObjectView>
        {
        }

        [Serializable]
        public class Settings
        {
            [FormerlySerializedAs("_bulletPrefab")] public AssetReference _bulletAssetReference;
            [FormerlySerializedAs("_minePrefab")] public AssetReference _mineAssetReference;
            [FormerlySerializedAs("_explosionPrefab")] public AssetReference _explosionAssetReference;
            [FormerlySerializedAs("_hitPrefab")] public AssetReference _hitAssetReference;
            [FormerlySerializedAs("_energyPrefab")] public AssetReference _energyAssetReference;
            public VisualEffectEntityFactoryFromSo _explosionEntityFactory;
            public VisualEffectEntityFactoryFromSo _hitEntityFactory;
        }
    }
}