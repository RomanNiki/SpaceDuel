using Extensions.Pause;
using Leopotam.Ecs;
using Zenject;

namespace Installers
{
    public class EcsWorldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PauseRegisterHandler>().AsSingle().CopyIntoAllSubContainers();
            Container.BindInterfacesAndSelfTo<EcsWorld>().AsSingle();
        }
    }
}