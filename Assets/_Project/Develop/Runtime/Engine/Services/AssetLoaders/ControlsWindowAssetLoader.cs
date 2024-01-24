using _Project.Develop.Runtime.Engine.UI.Controls;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Engine.Services.AssetLoaders
{
    public sealed class ControlsWindowAssetLoader : LocalAssetLoader
    {
        public async UniTask<ControlsWindowView> LoadAndInstantiate()
        {
            return await LoadAndInstantiateInternal<ControlsWindowView>(nameof(ControlsWindowView));
        }

        public void UnloadAndDestroy()
        {
            UnloadInternal();
        }
    }
}