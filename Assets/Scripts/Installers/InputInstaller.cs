using Zenject;

namespace Installers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        }
    }
}