using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Views;

namespace Extensions.AssetLoaders
{
    public class ControlsScreenProvider : LocalAssetLoader
    {
        private readonly AssetReference _reference;

        public ControlsScreenProvider(AssetReference reference)
        {
            _reference = reference;
        }
        
        public async UniTask<ControlsScreen> Load()
        {
            return await LoadAndInstantiateInternal<ControlsScreen>(_reference.AssetGUID);
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}