using Controller.Input;
using Model.Components.Events;
using Model.Components.Extensions;
using Model.Components.Extensions.UI;
using Model.Components.Tags.Buffs;
using Model.Components.Tags.Effects;
using Model.Components.Unit;
using Model.Systems;
using Model.Systems.Buffs;
using Model.Systems.Unit;
using Model.Systems.Unit.Collisions;
using Model.Systems.Unit.Destroy;
using Model.Systems.Unit.Input;
using Model.Systems.Unit.Movement;
using Model.Systems.VisualEffects;
using Model.Systems.Weapons;
using Model.Systems.Weapons.Particle;
using Model.Timers;
using Views.Systems;
using Views.Systems.Create.Buffs;
using Views.Systems.Create.Effects;
using Views.Systems.Create.Projectiles;
using Views.Systems.Update;
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
            Container.BindInterfacesAndSelfTo<Startup>().AsSingle().NonLazy();
        }

        private void AddSystems()
        {
            AddInputSystems();
            Container.BindInterfacesTo<EffectPauseSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;           
            Container.BindInterfacesTo<NozzleVFXSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;           
            AddShootSystems();
            AddTimers();
            Container.BindInterfacesTo<SunBuffEntityExecuteSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
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
            Container.BindInterfacesTo<DischargeEnergySystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;

            Container.BindInterfacesTo<SunChargeSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            Container.BindInterfacesTo<ChargeEnergySystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            Container.BindInterfacesTo<UpdateBarViewSystem<EnergyChangedEvent, Energy, EnergyBar>>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
        }

        private void AddViewCreateSystems()
        {
            Container.BindInterfacesTo<BulletViewCreateSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<MineViewCreateSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<ExplosionViewCreateSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<HitViewCreateSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;         
            Container.BindInterfacesTo<EnergyBuffViewCreateSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
        }

        private void AddTimers()
        {
            Container.BindInterfacesTo<TimerSystem<TimerBetweenShots>>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<TimerSystem<SunGravityResistTime>>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
            Container.BindInterfacesTo<TimerSystem<LifeTime>>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;     
            Container.BindInterfacesTo<TimerSystem<TimerBetweenSpawn>>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
        }

        private void AddShootSystems()
        {
            Container.BindInterfacesTo<OwnerIsAliveSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.Run;
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
            Container.BindInterfacesTo<InputPauseSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            Container.BindInterfacesTo<PrepareGameSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
            Container.BindInterfacesTo<ExecutePause>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.Run;
        }

        private void AddFixedSystems()
        {
            AddMoveSystems();
            AddCollisionsSystems();
            Container.BindInterfacesTo<ExecuteHitSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<DamageSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
            Container.BindInterfacesTo<UpdateBarViewSystem<HealthChangeEvent, Health, HealthBar>>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<DeathSystem>().AsSingle().NonLazy().BindInfo.Identifier = SystemsEnum.FixedRun;
      
            AddScoreSystems();
            AddDestroySystems();

        }

        private void AddRestartGameSystems()
        {
            Container.BindInterfacesTo<GameOverSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<RestartGameSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
        }

        private void AddDestroySystems()
        {
            Container.BindInterfacesTo<LifeTimeTimerSystem<VisualEffectTag>>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;          
            Container.BindInterfacesTo<LifeTimeTimerSystem<BuffTag>>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            AddRestartGameSystems();
            Container.BindInterfacesTo<ViewDestroySystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            AddExplosionSystems();
            Container.BindInterfacesTo<EntityDestroySystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
        }

        private void AddExplosionSystems()
        {
            Container.BindInterfacesTo<ExecuteExplosionSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<CameraExplosionImpulseSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
        }

        private void AddScoreSystems()
        {
            Container.BindInterfacesTo<CalculateScoreSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<UpdateScoreViewSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<UpdateCachedScoreSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
        }

        private void AddCollisionsSystems()
        {
            Container.BindInterfacesTo<SunTriggerEnterSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<DamageContainerTriggerSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<ShipCollisionSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;    
            Container.BindInterfacesTo<ChargeContainerTriggerSystem>().AsSingle().NonLazy().BindInfo.Identifier =
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
            Container.BindInterfacesTo<FollowSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
            Container.BindInterfacesTo<ExecuteMoveSystem>().AsSingle().NonLazy().BindInfo.Identifier =
                SystemsEnum.FixedRun;
        }
    }
}