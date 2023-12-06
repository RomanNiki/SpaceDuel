using _Project.Develop.Runtime.Engine.UI.Menu;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Engine.Services.AssetLoaders
{
    public class PauseMenuAssetLoader : LocalAssetLoader
    {
        public async UniTask<PauseMenuView> LoadAndInstantiate()
        {
            return await LoadAndInstantiateInternal<PauseMenuView>(nameof(PauseMenuView));
        }

        public void DestroyAndUnload()
        {
            UnloadInternal();
        }
    }
}