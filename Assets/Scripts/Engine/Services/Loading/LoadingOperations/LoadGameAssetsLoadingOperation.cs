using System;
using Core.Services;
using Cysharp.Threading.Tasks;
using Modules.Pooling.Core;

namespace Engine.Services.Loading.LoadingOperations
{
    public class LoadGameAssetsLoadingOperation : ILoadingOperation
    {
        private readonly ILoadingResource _assetsLoadProvider;
        
        public LoadGameAssetsLoadingOperation(IAssets assetsLoadProvider)
        {
            _assetsLoadProvider = assetsLoadProvider;
        }
        
        public string Description { get; } = "Load GameAssets";
        
        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            await _assetsLoadProvider.Load();
            onProgress?.Invoke(1f);
        }
    }
}