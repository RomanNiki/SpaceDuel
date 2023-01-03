using Leopotam.Ecs;
using Model.Pause;
using Zenject;

namespace Installers
{
    public class GameEcsWorldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PauseRegisterHandler>().AsSingle().CopyIntoAllSubContainers();
            var world = new EcsWorld();
            Container.BindInstance(world).AsSingle();
        }
    }
}