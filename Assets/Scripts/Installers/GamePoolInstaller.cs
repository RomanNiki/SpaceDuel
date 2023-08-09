using Extensions.AssetLoaders;
using Model.VisualEffects.Components.Tags;
using Model.Weapons.Components.Tags;
using Views;
using Views.Systems.Create.Buffs;
using Views.Systems.Create.Effects;
using Views.Systems.Create.Projectiles;
using Zenject;

namespace Installers
{
    public class GamePoolInstaller : MonoInstaller
    {
        [Inject] private GameAssetsLoadProvider _gameAssetsLoadProvider;
        private const string PoolObjectPrefix = "[Pooled]";

        public override void InstallBindings()
        {
            Container.BindFactory<ProjectileView, ProjectileViewFactory>()
                .FromPoolableMemoryPool<ProjectileView, BulletPool>(poolBinder =>
                    poolBinder.WithInitialSize(20).FromComponentInNewPrefab(_gameAssetsLoadProvider.BulletView)
                        .UnderTransformGroup(PoolObjectPrefix + "Bullets"))
                .WhenInjectedInto<ProjectileCreateSystem<BulletTag>>();
            Container.BindFactory<ProjectileView, ProjectileViewFactory>()
                .FromPoolableMemoryPool<ProjectileView, MinePool>(poolBinder =>
                    poolBinder.WithInitialSize(3).FromComponentInNewPrefab(_gameAssetsLoadProvider.MineView)
                        .UnderTransformGroup(PoolObjectPrefix + "Mines"))
                .WhenInjectedInto<ProjectileCreateSystem<MineTag>>();
            Container.BindFactory<VisualEffectView, VisualEffectViewFactory>()
                .FromPoolableMemoryPool<VisualEffectView, HitPool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromComponentInNewPrefab(_gameAssetsLoadProvider.HitView)
                        .UnderTransformGroup(PoolObjectPrefix + "Impacts"))
                .WhenInjectedInto<VisualEffectViewCreateSystem<HitTag>>();
            Container.BindFactory<VisualEffectView, VisualEffectViewFactory>()
                .FromPoolableMemoryPool<VisualEffectView, ExplosionPool>(poolBinder =>
                    poolBinder.WithInitialSize(10).FromComponentInNewPrefab(_gameAssetsLoadProvider.ExplosionView)
                        .UnderTransformGroup(PoolObjectPrefix + "Explosions"))
                .WhenInjectedInto<VisualEffectViewCreateSystem<ExplosionTag>>();
            Container.BindFactory<GameObjectView, GameObjectViewFactory>()
                .FromPoolableMemoryPool<GameObjectView, EnergyBuffPool>(poolBinder =>
                    poolBinder.WithInitialSize(5).FromComponentInNewPrefab(_gameAssetsLoadProvider.EnergyBuffView)
                        .UnderTransformGroup(PoolObjectPrefix + "Buffs"))
                .WhenInjectedInto<EnergyBuffViewCreateSystem>();
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
    }
}