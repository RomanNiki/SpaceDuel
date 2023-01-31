using System;
using Extensions.AssetLoaders;
using UnityEngine;
using Views;
using Views.Systems.Create.Buffs;
using Views.Systems.Create.Effects;
using Views.Systems.Create.Projectiles;
using Zenject;

namespace Installers
{
    public class GamePoolInstaller : MonoInstaller, IDisposable
    {
        [Inject] private GameAssetsLoadProvider _gameAssetsLoadProvider;

        public override void InstallBindings()
        {
            Debug.Log(_gameAssetsLoadProvider.BulletView);
            Container.BindFactory<ProjectileView, ProjectileView.Factory>()
                .FromPoolableMemoryPool<ProjectileView, BulletPool>(poolBinder =>
                    poolBinder.WithInitialSize(20).FromComponentInNewPrefab(_gameAssetsLoadProvider.BulletView))
                .WhenInjectedInto<BulletViewCreateSystem>().NonLazy();
            Container.BindFactory<ProjectileView, ProjectileView.Factory>()
                .FromPoolableMemoryPool<ProjectileView, MinePool>(poolBinder =>
                    poolBinder.WithInitialSize(3).FromComponentInNewPrefab(_gameAssetsLoadProvider.MineView))
                .WhenInjectedInto<MineViewCreateSystem>().NonLazy();
            Container.BindFactory<VisualEffectView, VisualEffectView.Factory>()
                .FromPoolableMemoryPool<VisualEffectView, HitPool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromComponentInNewPrefab(_gameAssetsLoadProvider.HitView))
                .WhenInjectedInto<HitViewCreateSystem>().NonLazy();
            Container.BindFactory<VisualEffectView, VisualEffectView.Factory>()
                .FromPoolableMemoryPool<VisualEffectView, ExplosionPool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromComponentInNewPrefab(_gameAssetsLoadProvider.ExplosionView))
                .WhenInjectedInto<ExplosionViewCreateSystem>().NonLazy();
            Container.BindFactory<GameObjectView, GameObjectView.Factory>()
                .FromPoolableMemoryPool<GameObjectView, EnergyBuffPool>(poolBinder =>
                    poolBinder.WithInitialSize(5).FromComponentInNewPrefab(_gameAssetsLoadProvider.EnergyBuffView))
                .WhenInjectedInto<EnergyBuffViewCreateSystem>().NonLazy();
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
        
        public void Dispose()
        {
            _gameAssetsLoadProvider?.UnloadAssets();
        }
    }
}