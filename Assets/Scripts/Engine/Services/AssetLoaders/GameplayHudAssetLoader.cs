using Cysharp.Threading.Tasks;
using Engine.UI;

namespace Engine.Services.AssetLoaders
{
    public class GameplayHudAssetLoader : LocalAssetLoader
    {
        public async UniTask<GameplayHud> LoadAndInstantiate()
        {
            return await LoadAndInstantiateInternal<GameplayHud>(nameof(GameplayHud));
        }

        public void DestroyAndUnload()
        {
            UnloadInternal();
        }
    }
}