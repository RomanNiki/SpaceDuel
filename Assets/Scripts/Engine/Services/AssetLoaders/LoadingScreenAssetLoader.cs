using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Engine.Services.Loading;
using Engine.Services.Loading.LoadingOperations;

namespace Engine.Services.AssetLoaders
{
    public sealed class LoadingScreenAssetLoader : LocalAssetLoader
    {
        public async UniTask LoadAndDestroy(Queue<ILoadingOperation> loadingOperations)
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