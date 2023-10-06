using Cysharp.Threading.Tasks;
using Engine.UI;

namespace Engine.Services.AssetLoaders
{
    public class ControlsWindowAssetLoader : LocalAssetLoader
    {
        public async UniTask<ControlsWindow> LoadAndInstantiate()
        {
            return await LoadAndInstantiateInternal<ControlsWindow>(nameof(ControlsWindow));
        }

        public void DestroyAndUnload()
        {
            UnloadInternal();
        }
    }
}