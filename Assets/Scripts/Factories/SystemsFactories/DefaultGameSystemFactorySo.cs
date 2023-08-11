using Core.Collisions.Systems;
using Core.Damage.Components;
using Core.Damage.Systems;
using Core.EnergyLimits.Components;
using Core.EnergyLimits.Systems;
using Core.Extensions.Systems;
using Core.Input.Components;
using Core.Input.Systems;
using Core.Movement;
using Core.Movement.Systems;
using Core.Sun.Systems;
using Input;
using Scellecs.Morpeh;
using UnityEngine;

namespace Factories.SystemsFactories
{
    [CreateAssetMenu(menuName = "SystemsFactory/Default")]
    public sealed class DefaultGameSystemFactorySo : SystemsFactoryBaseSo
    {
        private static SystemsGroup CreateUpdateSystems(SystemsGroup systemsGroup)
        {
            CreateInputSystems(systemsGroup);
            systemsGroup.AddSystem(new MoveDischargeSystem());
            CreateDestroyInputEvents(systemsGroup);
            CreateDamageSystems(systemsGroup);
            CreateEnergySystems(systemsGroup);
            systemsGroup.AddSystem(new DelHere<HealthChangedEvent>());
            systemsGroup.AddSystem(new DelHere<EnergyChangedEvent>());
            return systemsGroup;
        }

        private static void CreateEnergySystems(SystemsGroup systems)
        {
            systems.AddSystem(new SunChargeSystem());
            systems.AddSystem(new DischargeSystem());
            systems.AddSystem(new ChargeSystem());
        }

        private static void CreateDestroyInputEvents(SystemsGroup systems)
        {
            systems.AddSystem(new DelHere<InputAccelerateStartedEvent>());
            systems.AddSystem(new DelHere<InputAccelerateCanceledEvent>());
            systems.AddSystem(new DelHere<InputRotateStartedEvent>());
            systems.AddSystem(new DelHere<InputRotateCanceledEvent>());
            systems.AddSystem(new DelHere<InputShootStartedEvent>());
            systems.AddSystem(new DelHere<InputShootCanceledEvent>());
        }

        private SystemsGroup CreateFixedUpdateSystems(SystemsGroup systems, IMoveLoopService loopService)
        {
            CreateMoveSystems(systems, loopService);
            CreateCollisionsSystems(systems);
            return systems;
        }

        private static void CreateDamageSystems(SystemsGroup systems)
        {
            systems.AddSystem(new InstantlyKillSystem());
            systems.AddSystem(new DamageSystem());
            systems.AddSystem(new CheckDeathSystem());
            systems.AddSystem(new DeathSystem());
            systems.AddSystem(new DestroySystem());
        }

        private static void CreateCollisionsSystems(SystemsGroup systems)
        {
            systems.AddSystem(new GravityPointCollisionSystem());
            systems.AddSystem(new PlayerCollisionSystem());
            systems.AddSystem(new DamagerCollisionSystem());
        }

        private static void CreateInputSystems(SystemsGroup systems)
        {
            systems.AddInitializer(new InputSystem(new PlayerInput()));
            systems.AddSystem(new InputAccelerateSystem());
            systems.AddSystem(new InputRotateSystem());
        }

        private static void CreateMoveSystems(SystemsGroup systems, IMoveLoopService loopService)
        {
            systems.AddSystem(new RotateSystem());
            systems.AddSystem(new ForceSystem());
            systems.AddSystem(new GravitySystem());
            systems.AddSystem(new AccelerationSystem());
            systems.AddSystem(new VelocitySystem());
            systems.AddSystem(new FrictionSystem());
            systems.AddSystem(new MoveClampSystem(loopService));
            systems.AddSystem(new ExecuteMoveSystem());
        }

        public override SystemsGroup CreateGameSystemGroup(World world, IMoveLoopService moveLoopService)
        {
            var systemGroup = world.CreateSystemsGroup();
            CreateUpdateSystems(systemGroup);
            CreateFixedUpdateSystems(systemGroup, moveLoopService);
            return systemGroup;
        }
    }
}