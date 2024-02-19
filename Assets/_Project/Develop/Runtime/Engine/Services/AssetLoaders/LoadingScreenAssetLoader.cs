using System.Collections.Generic;
using _Project.Develop.Runtime.Engine.Services.Loading;
using _Project.Develop.Runtime.Engine.Services.Loading.LoadingOperations;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Engine.Services.AssetLoaders
{
    public sealed class LoadingScreenAssetLoader : LocalAssetLoader
    {
        public async UniTaskVoid LoadAndDestroy(Queue<ILoadingOperation> loadingOperations)
        {
            var loadingScreen = await Load();
            await loadingScreen.Load(loadingOperations);
            Unload();
        }

        public async UniTask<LoadingScreen> Load()
        {
            return await LoadAndInstantiateInternal<LoadingScreen>(nameof(LoadingScreen));
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}