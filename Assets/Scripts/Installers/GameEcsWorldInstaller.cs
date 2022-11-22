using Extensions;
using Leopotam.Ecs;
using Systems;
using Systems.Unit;
using Systems.Unit.Movement;
using Systems.Unit.Movement.Input;
using Zenject;

namespace Installers
{
    public class GameEcsWorldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EcsWorld>().FromNew().AsSingle();
            Container.Bind<EcsSystems>().WithId(SystemsEnum.Run).AsCached();
            Container.Bind<EcsSystems>().WithId(SystemsEnum.FixedRun).AsCached();
            
            Container.BindInterfacesTo<LoopedMoveSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<RotateToVelocitySystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            
            Container.BindInterfacesTo<InputSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            Container.BindInterfacesTo<InputAccelerateSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            Container.BindInterfacesTo<InputRotateSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            
            Container.BindInterfacesTo<PlayerForceSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<PlayerRotateSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<AccelerationSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<VelocitySystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<SunGravitySystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<FrictionSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<ExecuteMoveSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;

            Container.BindInterfacesTo<EnergySpendSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            Container.BindInterfacesTo<EnergyChargeSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
      
            
            Container.BindInterfacesAndSelfTo<SystemRegisterHandler>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameStartup>().AsSingle().NonLazy();
        }
    }
}