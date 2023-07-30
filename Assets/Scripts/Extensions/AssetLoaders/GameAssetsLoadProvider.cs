using System;
using Cysharp.Threading.Tasks;
using Extensions.UI;
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
        public Transform FirstPlayer { get; private set; }
        public Transform SecondPlayer { get; private set; }
        public PlayerUIBars PlayerUIBar { get; private set; }

        public async UniTask LoadAssets()
        {
            BulletView = await LoadAsset<ProjectileView>(_settings.BulletAssetReference);
            MineView = await LoadAsset<ProjectileView>(_settings.MineAssetReference);
            HitView = await LoadAsset<VisualEffectView>(_settings.HitAssetReference);
            ExplosionView = await LoadAsset<VisualEffectView>(_settings.ExplosionAssetReference);
            EnergyBuffView = await LoadAsset<GameObjectView>(_settings.EnergyAssetReference);
            FirstPlayer = await LoadAsset<Transform>(_settings.FirstPlayerAssetReference);
            SecondPlayer = await LoadAsset<Transform>(_settings.SecondPlayerAssetReference);
            PlayerUIBar = await LoadAsset<PlayerUIBars>(_settings.PlayerUIBarAssetReference);
        }

        public void UnloadAssets()
        {
            _settings.BulletAssetReference.ReleaseAsset();
            _settings.MineAssetReference.ReleaseAsset();
            _settings.HitAssetReference.ReleaseAsset();
            _settings.ExplosionAssetReference.ReleaseAsset();
            _settings.EnergyAssetReference.ReleaseAsset();
            _settings.FirstPlayerAssetReference.ReleaseAsset();
            _settings.SecondPlayerAssetReference.ReleaseAsset();
            _settings.PlayerUIBarAssetReference.ReleaseAsset();
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
            public AssetReference FirstPlayerAssetReference;
            public AssetReference SecondPlayerAssetReference;
            public AssetReference PlayerUIBarAssetReference;
        }
    }
}