using Cysharp.Threading.Tasks;
using Extensions.MappingUnityToModel;
using Leopotam.Ecs;

namespace Extensions.AssetLoaders
{
    public class PauseMenuProvider : LocalAssetLoader
    {
        public async UniTask<PauseMenu> Load(EcsWorld world)
        {
            var menu = await LoadAndInstantiateInternal<PauseMenu>(nameof(PauseMenu));
            menu.SetWorld(world);
            return menu;
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}