using System;
using _Project.Develop.Runtime.Core.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Develop.Runtime.Engine.Services.Loading.LoadingOperations
{
    public class UnloadGameAssetsLoadingOperation : ILoadingOperation
    {
        private readonly IAssets _assets;

        public UnloadGameAssetsLoadingOperation(IAssets assets)
        {
            _assets = assets;
        }

        public string Description => "Unloading Assets";

        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            _assets.Dispose();
            await UniTask.Yield();
            onProgress?.Invoke(1f);
        }
    }
}