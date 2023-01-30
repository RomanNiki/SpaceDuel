using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Views;
using Zenject;

namespace Extensions.AssetLoaders
{
    public class GameAssetsLoadProvider
    {
        [Inject] private readonly Settings _settings;
        public ProjectileView BulletView { get; private set; }
        public ProjectileView MineView { get; private set; }
        public VisualEffectView HitView { get; private set; }
        public VisualEffectView ExplosionView { get; private set; }
        public GameObjectView EnergyBuffView { get; private set; }

        public async UniTask LoadAssets()
        {
            BulletView = await LoadAsset<ProjectileView>(_settings.BulletAssetReference);
            MineView = await LoadAsset<ProjectileView>(_settings.MineAssetReference);
            HitView = await LoadAsset<VisualEffectView>(_settings.HitAssetReference);
            ExplosionView = await LoadAsset<VisualEffectView>(_settings.ExplosionAssetReference);
            EnergyBuffView = await LoadAsset<GameObjectView>(_settings.EnergyAssetReference);
        }

        public void UnloadAssets()
        {
            _settings.BulletAssetReference.ReleaseAsset();
            _settings.MineAssetReference.ReleaseAsset();
            _settings.HitAssetReference.ReleaseAsset();
            _settings.ExplosionAssetReference.ReleaseAsset();
            _settings.EnergyAssetReference.ReleaseAsset();
        }

        private static async UniTask<T> LoadAsset<T>(AssetReference assetReference)
            where T : Component
        {
            var handle = assetReference.LoadAssetAsync<GameObject>().Task;
            var asset = await handle;
            if (asset.TryGetComponent<T>(out var gameObject))
            {
                return gameObject;
            }

            throw new NullReferenceException(
                "Object of type {typeof(T)} is null on attempt to load it from addresables");
        }

        [Serializable]
        public class Settings
        {
            public AssetReference BulletAssetReference;
            public AssetReference MineAssetReference;
            public AssetReference ExplosionAssetReference;
            public AssetReference HitAssetReference;
            public AssetReference EnergyAssetReference;
        }
    }
}