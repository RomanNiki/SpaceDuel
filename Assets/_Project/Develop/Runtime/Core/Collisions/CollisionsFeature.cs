using _Project.Develop.Runtime.Core.Buffs.Components;
using _Project.Develop.Runtime.Core.Collisions.Components;
using _Project.Develop.Runtime.Core.Collisions.Strategies;
using _Project.Develop.Runtime.Core.Collisions.Systems;
using _Project.Develop.Runtime.Core.Movement.Components.Gravity;
using _Project.Develop.Runtime.Core.Player.Components;
using _Project.Develop.Runtime.Core.Weapon.Components;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Collisions
{
    public class CollisionsFeature : FixedUpdateFeature
    {
        protected override void Initialize()
        {
            var damageKillSelfStrategy = new CombineStrategies(new DamageTargetStrategy(), new KillSenderStrategy());
            var excludeStrategy = new InvisibleStrategy(damageKillSelfStrategy);
            AddSystem(new TriggerSystem<ProjectileTag, ProjectileTag>(excludeStrategy));
            AddSystem(new TriggerSystem<ProjectileTag, PlayerTag>(excludeStrategy));
            AddSystem(new TriggerSystem<ProjectileTag, BuffTag>(new CombineStrategies(new DestroyTargetStrategy(), new KillSenderStrategy())));
            AddSystem(new TriggerSystem<GravityPoint, ProjectileTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<GravityPoint, PlayerTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<BuffTag, PlayerTag>(new CreateBuffStrategy()));
            AddSystem(new TriggerSystem<PlayerTag, BuffTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<PlayerTag, PlayerTag>(new DamageTargetByHealthStrategy()));
            RegisterRequest<TriggerEnterRequest>();
        }
    }
}