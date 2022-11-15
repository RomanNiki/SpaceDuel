using Leopotam.Ecs;
using Systems;
using Systems.Player.Movement;
using Zenject;

namespace Installers
{
    public class GameEcsWorldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EcsWorld>().FromNew().AsSingle();
            Container.Bind<EcsSystems>().AsSingle();
            
            Container.BindInterfacesTo<LoopedMoveSystem>().AsSingle().NonLazy();
         
            Container.BindInterfacesTo<InputSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InputAccelerateSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InputRotateSystem>().AsSingle().NonLazy();
     
            Container.BindInterfacesTo<PlayerMoveSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnergySpendSystem>().AsSingle().NonLazy();
      
            
            Container.BindInterfacesAndSelfTo<SystemRegisterHandler>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameStartup>().AsSingle().NonLazy();
        }
    }
}