using System;
using Cysharp.Threading.Tasks;
using Extensions.AssetLoaders;

namespace Extensions.Loading.LoadingOperations
{
    public class UnloadGameAssets : ILoadingOperation
    {
        private readonly GameAssetsLoadProvider _assetsLoadProvider;

        public UnloadGameAssets(GameAssetsLoadProvider assetsLoadProvider)
        {
            _assetsLoadProvider = assetsLoadProvider;
        }

        public string Description { get; } = "Unloading Assets";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            _assetsLoadProvider.UnloadAssets();
            onProgress?.Invoke(1f);
        }
    }
}