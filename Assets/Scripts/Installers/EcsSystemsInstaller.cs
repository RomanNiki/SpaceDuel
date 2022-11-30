using Controller.Input;
using Model.Components.Extensions;
using Model.Systems;
using Model.Systems.Unit;
using Model.Systems.Unit.Collisions;
using Model.Systems.Unit.Input;
using Model.Systems.Unit.Movement;
using Model.Systems.Weapons;
using Model.Systems.Weapons.Particle;
using Model.Timers;
using Views.Systems;
using Views.Systems.Create;
using Zenject;

namespace Installers
{
    public class EcsSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            AddFixedSystems();
            AddSystems();
            
            Container.BindInterfacesAndSelfTo<SystemRegisterHandler>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameStartup>().AsSingle().NonLazy();
        }

        private void AddSystems()
        {
            AddInputSystems();
            AddShootSystems();
            AddTimers();
            AddViewCreateSystems();

            Container.BindInterfacesTo<CheckSunGravityResistSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;

            AddEnergySystems();
        }

        private void AddEnergySystems()
        {
            Container.BindInterfacesTo<MoveEnergyDischargeSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<WeaponEnergyDischargeSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<DischargeEnergySystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;

            Container.BindInterfacesTo<SunChargeSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            Container.BindInterfacesTo<ChargeEnergySystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
        }

        private void AddViewCreateSystems()
        {
            Container.BindInterfacesTo<BulletViewCreateSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<MineViewCreateSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
        }

        private void AddTimers()
        {
            Container.BindInterfacesTo<TimerSystem<TimerBetweenShots>>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<TimerSystem<SunGravityResistTime>>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
        }

        private void AddShootSystems()
        {
            Container.BindInterfacesTo<CheckOwnerEnergyBlockSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<ShootDeniedTimeBetweenShotsSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<ExecuteShootSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            Container.BindInterfacesTo<GunTimerBetweenShotsStartSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
        }

        private void AddInputSystems()
        {
            Container.BindInterfacesTo<InputSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            Container.BindInterfacesTo<InputAccelerateSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<InputRotateSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            Container.BindInterfacesTo<InputShootSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
        }

        private void AddFixedSystems()
        {
            AddMoveSystems();
            CollisionsSystems();
            Container.BindInterfacesTo<DamageSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<DeathSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<CalculateScoreSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<UpdateScoreViewSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<EntityDestroySystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
        }

        private void CollisionsSystems()
        {
            Container.BindInterfacesTo<SunTriggerEnterSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<BulletTriggerSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<ShipCollisionSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
        }

        private void AddMoveSystems()
        {
            Container.BindInterfacesTo<LoopedMoveSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<SunGravitySystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<RotateToVelocitySystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<PlayerForceSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<PlayerRotateSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<AccelerationSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<FrictionSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<VelocitySystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<ExecuteMoveSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
        }
    }
}