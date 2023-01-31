using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Extensions.Loading;
using Extensions.Loading.LoadingOperations;

namespace Extensions.AssetLoaders
{
    public class LoadingScreenProvider : LocalAssetLoader
    {
        public async UniTask LoadAndDestroy(Queue<ILoadingOperation> loadingOperations)
        {
            var loadingScreen = await Load();
            await loadingScreen.Load(loadingOperations);
            Unload();
        }
        
        public UniTask<LoadingScreen> Load()
        {
            return LoadAndInstantiateInternal<LoadingScreen>(nameof(LoadingScreen));
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}