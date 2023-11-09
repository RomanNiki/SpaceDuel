using Core.Buffs.Components;
using Core.Collisions.Components;
using Core.Collisions.Strategies;
using Core.Collisions.Systems;
using Core.Movement.Components.Gravity;
using Core.Player.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Collisions
{
    public class CollisionsFeature : FixedUpdateFeature
    {
        protected override void Initialize()
        {
            var damageKillSelfStrategy = new CombineStrategies(new DamageTargetStrategy(), new KillSenderStrategy());
            var excludeStrategy = new InvisibleStrategy(damageKillSelfStrategy);
            AddSystem(new TriggerSystem<ProjectileTag, ProjectileTag>(excludeStrategy));
            AddSystem(new TriggerSystem<ProjectileTag, PlayerTag>(excludeStrategy));
            AddSystem(new TriggerSystem<ProjectileTag, BuffTag>(excludeStrategy));
            AddSystem(new TriggerSystem<GravityPoint, ProjectileTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<GravityPoint, PlayerTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<BuffTag, PlayerTag>(new CreateBuffStrategy()));
            AddSystem(new TriggerSystem<PlayerTag, BuffTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<PlayerTag, PlayerTag>(new DamageTargetByHealthStrategy()));
            RegisterRequest<TriggerEnterRequest>();
        }
    }
}