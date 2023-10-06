using System;
using Core.Services;
using Cysharp.Threading.Tasks;

namespace Engine.Services.Loading.LoadingOperations
{
    public class UnloadGameAssets : ILoadingOperation
    {
        private readonly IAssets _assets;

        public UnloadGameAssets(IAssets assets)
        {
            _assets = assets;
        }

        public string Description { get; } = "Unloading Assets";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            _assets.Dispose();
            await UniTask.Yield();
            onProgress?.Invoke(1f);
        }
    }
}