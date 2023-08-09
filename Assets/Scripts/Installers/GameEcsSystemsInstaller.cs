using Controller;
using Controller.Input;
using EntityToGameObject;
using Extensions;
using Extensions.UI;
using Model;
using Model.Buffs;
using Model.Scores;
using Model.Timers;
using Model.Timers.Components;
using Model.Unit.Collisions;
using Model.Unit.Damage;
using Model.Unit.Damage.Components;
using Model.Unit.Damage.Components.Events;
using Model.Unit.Destroy;
using Model.Unit.EnergySystems;
using Model.Unit.EnergySystems.Components;
using Model.Unit.EnergySystems.Components.Events;
using Model.Unit.Input;
using Model.Unit.Movement;
using Model.Unit.SunEntity;
using Model.VisualEffects;
using Model.VisualEffects.Components.Tags;
using Model.Weapons;
using Model.Weapons.Components;
using Model.Weapons.Components.Tags;
using Views.Systems;
using Views.Systems.Create.Buffs;
using Views.Systems.Create.Effects;
using Views.Systems.Create.Projectiles;
using Views.Systems.Update;
using Zenject;

namespace Installers
{
    public class GameEcsSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            AddInitSystems();
            AddFixedSystems();
            AddSystems();
            Container.BindInterfacesAndSelfTo<MoveClamper>().AsSingle();
        }

        private void AddInitSystems()
        {
            Container.AddInitSystem<PlayerInitSystem>();
        }

        private void AddSystems()
        {
            Container.AddRunSystem<GameStateSystem>();           
            AddInputSystems();
            Container.AddRunSystem<EffectPauseSystem>();
            Container.AddRunSystem<NozzleVFXSystem>();
            AddShootSystems();
            Container.AddRunSystem<GunSoundSystem>();
            Container.AddRunSystem<PlayerMoveSoundSystem>();
            AddTimers();
            Container.AddRunSystem<SunBuffEntityExecuteSystem>();
            AddViewCreateSystems();
            Container.AddRunSystem<CheckSunGravityResistSystem>();

            AddEnergySystems();
        }

        private void AddEnergySystems()
        {
            Container.AddRunSystem<MoveEnergyDischargeSystem>();
            Container.AddRunSystem<WeaponEnergyDischargeSystem>();
            Container.AddRunSystem<SunDischargeSystem>();
            Container.AddRunSystem<DischargeEnergySystem>();
            Container.AddRunSystem<SunChargeSystem>();
            Container.AddRunSystem<ChargeEnergySystem>();
            Container.AddRunSystem<UpdateBarViewSystem<EnergyChangedEvent, Energy, EnergyBar>>();
        }

        private void AddViewCreateSystems()
        {
            Container.AddRunSystem<ProjectileCreateSystem<BulletTag>>();
            Container.AddRunSystem<ProjectileCreateSystem<MineTag>>();
            Container.AddRunSystem<VisualEffectViewCreateSystem<ExplosionTag>>();
            Container.AddRunSystem<VisualEffectViewCreateSystem<HitTag>>();
            Container.AddRunSystem<EnergyBuffViewCreateSystem>();
        }

        private void AddTimers()
        {
            Container.AddRunSystem<TimerSystem<TimerBetweenShots>>();
            Container.AddRunSystem<TimerSystem<LifeTimer>>();
            Container.AddRunSystem<TimerSystem<TimerBetweenSpawn>>();
        }

        private void AddShootSystems()
        {
            Container.AddRunSystem<OwnerIsAliveSystem>();
            Container.AddRunSystem<CheckOwnerEnergyBlockSystem<WeaponType>>();
            Container.AddRunSystem<ShootDeniedTimeBetweenShotsSystem>();
            Container.AddRunSystem<ExecuteShootSystem>();
            Container.AddRunSystem<GunTimerBetweenShotsStartSystem>();
        }

        private void AddInputSystems()
        {
            Container.AddInitSystem<InputSystem>();
            Container.AddRunSystem<InputAccelerateSystem>();
            Container.AddRunSystem<InputRotateSystem>();
            Container.AddRunSystem<InputShootSystem>();
            Container.AddRunSystem<InputPauseSystem>();
        }

        private void AddFixedSystems()
        {
            AddMoveSystems();
            Container.AddFixedSystem<ExecuteHitSystem>();
            AddCollisionsSystems();
            Container.AddFixedSystem<DamageSystem>();
            Container.AddFixedSystem<UpdateBarViewSystem<HealthChangeEvent, Health, HealthBar>>();
            Container.AddFixedSystem<DeathSystem>();
            AddScoreSystems();
            AddDestroySystems();
        }

        private void AddRestartGameSystems()
        {
            Container.AddFixedSystem<GameOverSystem>();
        }

        private void AddDestroySystems()
        {
            Container.AddFixedSystem<LifeTimeTimerSystem>();
            AddRestartGameSystems();
            Container.AddFixedSystem<ViewDestroySystem>();
            AddExplosionSystems();
            Container.AddFixedSystem<EntityDestroySystem>();
        }

        private void AddExplosionSystems()
        {
            Container.AddFixedSystem<ExecuteExplosionSystem>();
            Container.AddFixedSystem<CameraExplosionImpulseSystem>();
        }

        private void AddScoreSystems()
        {
            Container.AddFixedSystem<CalculateScoreSystem>();
            Container.AddFixedSystem<UpdateScoreViewSystem>();
            Container.AddFixedSystem<UpdateCachedScoreSystem>();
        }

        private void AddCollisionsSystems()
        {
            Container.AddFixedSystem<SunTriggerEnterSystem>();
            Container.AddFixedSystem<DamageContainerTriggerSystem>();
            Container.AddFixedSystem<ShipCollisionSystem>();
            Container.AddFixedSystem<ChargeContainerTriggerSystem>();
        }

        private void AddMoveSystems()
        {
            Container.AddFixedSystem<ClampMoveSystem>();
            Container.AddFixedSystem<SunGravitySystem>();
            Container.AddFixedSystem<RotateToVelocitySystem>();
            Container.AddFixedSystem<PlayerForceSystem>();
            Container.AddFixedSystem<PlayerRotateSystem>();
            Container.AddFixedSystem<AccelerationSystem>();
            Container.AddFixedSystem<FrictionSystem>();
            Container.AddFixedSystem<VelocitySystem>();
            Container.AddFixedSystem<FollowSystem>();
            Container.AddFixedSystem<ExecuteMoveSystem>();
        }
    }
}