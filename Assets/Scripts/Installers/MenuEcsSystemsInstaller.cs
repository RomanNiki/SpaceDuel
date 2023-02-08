using Extensions;
using Views.UI.Menu;
using Zenject;

namespace Installers
{
    public class MenuEcsSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.AddRunSystem<CloseGameSystem>();
            Container.AddRunSystem<OptionsSystem>();
            Container.AddRunSystem<StartGameSystem>();
        }
    }
}