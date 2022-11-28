using Leopotam.Ecs;
using Zenject;

namespace Installers
{
    public class GameEcsWorldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EcsWorld>().FromNew().AsSingle();
        }
    }
}