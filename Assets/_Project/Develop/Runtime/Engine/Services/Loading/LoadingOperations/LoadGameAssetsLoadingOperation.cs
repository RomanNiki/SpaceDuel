using System;
using _Project.Develop.Modules.Pooling.Core;
using _Project.Develop.Runtime.Core.Services;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Engine.Services.Loading.LoadingOperations
{
    public class LoadGameAssetsLoadingOperation : ILoadingOperation
    {
        private readonly ILoadingResource _assetsLoadProvider;

        public LoadGameAssetsLoadingOperation(ILoadingResource assetsLoadProvider)
        {
            _assetsLoadProvider = assetsLoadProvider;
        }

        public string Description => "Load GameAssets";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            await _assetsLoadProvider.Load();
            onProgress?.Invoke(1f);
        }
    }
}