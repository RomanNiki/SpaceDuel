using System;
using Cysharp.Threading.Tasks;
using Extensions.AssetLoaders;

namespace Extensions.Loading.LoadingOperations
{
    public class LoadGameAssets : ILoadingOperation
    {
        private readonly GameAssetsLoadProvider _assetsLoadProvider;
        
        public LoadGameAssets(GameAssetsLoadProvider assetsLoadProvider)
        {
            _assetsLoadProvider = assetsLoadProvider;
        }
        public string Description { get; } = "Load GameAssets";
        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            await _assetsLoadProvider.LoadAssets();
            onProgress?.Invoke(1f);
        }
    }
}