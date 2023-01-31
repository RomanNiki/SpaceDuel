using Extensions;
using Model.Extensions;
using Views.UI.Menu;
using Zenject;

namespace Installers
{
    public class MenuEcsSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CloseGameSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;           
            Container.BindInterfacesTo<StartGameSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;    
            
            Container.BindInterfacesAndSelfTo<SystemRegisterHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Startup>().AsSingle().NonLazy();
        }
    }
}