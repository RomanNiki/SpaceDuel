using Core.Characteristics.EnergyLimits.Components;
using Core.Effects.Components;
using Core.Extensions;
using Core.Movement.Components.Gravity;
using Core.Timers.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Services.Factories.EntitiesFactories.Projectiles
{
    [CreateAssetMenu(fileName = "Mine", menuName = "SpaceDuel/Projectiles/Mine", order = 10)]
    public class MineEntityFactorySo : ProjectileEntityFactorySo
    {
        [SerializeField] private float _startEnergy = 40;
        [SerializeField] private float _invisibleTimeSec = 0.5f;

        protected override void OnCreateProjectileEntity(Entity entity, in World world)
        {
            world.AddComponentToEntity(entity, new MineTag());
            world.AddComponentToEntity(entity, new SunDischargeTag());
            world.AddComponentToEntity(entity, new ExplosiveTag());
            world.AddComponentToEntity(entity, new GravityResistTag());
            world.AddComponentToEntity(entity, new Energy(_startEnergy));
            world.AddComponentToEntity(entity, new Timer<InvisibleTimer> { TimeLeftSec = _invisibleTimeSec });
        }
    }
}