using Extensions.Pause;
using Leopotam.Ecs;
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