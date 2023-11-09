using Cysharp.Threading.Tasks;
using Engine.UI.Controls;

namespace Engine.Services.AssetLoaders
{
    public sealed class ControlsWindowAssetLoader : LocalAssetLoader
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