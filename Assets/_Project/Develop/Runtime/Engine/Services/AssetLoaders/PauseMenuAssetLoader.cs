using _Project.Develop.Runtime.Engine.UI.Menu;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Services.AssetLoaders
{
    public class PauseMenuAssetLoader : ResolvedGameObjectLocalAssetLoader
    {
        public PauseMenuAssetLoader(IObjectResolver resolver) : base(resolver)
        {
        }
        
        public async UniTask<PauseMenuView> LoadAndInstantiate()
        {
            return await LoadAndInstantiateWithInjection<PauseMenuView>(nameof(PauseMenuView));
        }

        public void UnloadAndDestroy()
        {
            UnloadInternal();
        }
    }
}