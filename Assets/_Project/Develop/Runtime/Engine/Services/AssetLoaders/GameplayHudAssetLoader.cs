using _Project.Develop.Runtime.Engine.UI;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Engine.Services.AssetLoaders
{
    public sealed class GameplayHudAssetLoader : LocalAssetLoader
    {
        public async UniTask<GameplayHud> LoadAndInstantiate()
        {
            return await LoadAndInstantiateInternal<GameplayHud>(nameof(GameplayHud));
        }

        public void UnloadAndDestroy()
        {
            UnloadInternal();
        }
    }
}