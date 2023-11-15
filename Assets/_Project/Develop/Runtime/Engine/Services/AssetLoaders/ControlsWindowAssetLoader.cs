using _Project.Develop.Runtime.Engine.UI.Controls;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Engine.Services.AssetLoaders
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