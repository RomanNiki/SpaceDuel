using Cysharp.Threading.Tasks;
using Views;

namespace Extensions.AssetLoaders
{
    public sealed class PrepareGameScreenProvider : LocalAssetLoader
    {
        public UniTask<PrepareScreen> Load()
        {
            return LoadAndInstantiateInternal<PrepareScreen>(nameof(PrepareScreen));
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}