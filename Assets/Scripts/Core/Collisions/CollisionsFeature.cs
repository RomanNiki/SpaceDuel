using Core.Buffs.Components;
using Core.Characteristics.Player.Components;
using Core.Collisions.Components;
using Core.Collisions.Strategies;
using Core.Collisions.Systems;
using Core.Movement.Components.Gravity;
using Core.Weapon.Components;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Collisions
{
    public class CollisionsFeature : FixedUpdateFeature
    {
        protected override void Initialize()
        {
            
            AddSystem(new TriggerSystem<ProjectileTag, ProjectileTag>(new DamageTargetStrategy()));
            AddSystem(new TriggerSystem<ProjectileTag, PlayerTag>(new DamageTargetStrategy()));
            AddSystem(new TriggerSystem<ProjectileTag, EnergyBuffTag>(new DamageTargetStrategy()));
            AddSystem(new TriggerSystem<GravityPoint, ProjectileTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<GravityPoint, PlayerTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<EnergyBuffTag, PlayerTag>(new ChargeTargetStrategy()));
            AddSystem(new TriggerSystem<PlayerTag, EnergyBuffTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<PlayerTag, PlayerTag>(new DamageTargetByHealthStrategy()));
            RegisterRequest<TriggerEnterRequest>();
        }
    }
}