using Cysharp.Threading.Tasks;
using Views;

namespace Extensions.AssetLoaders
{
    public class ControlsScreenProvider : LocalAssetLoader
    {
        public UniTask<ControlsScreen> Load()
        {
            return LoadAndInstantiateInternal<ControlsScreen>(nameof(ControlsScreen));
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}