using Core.Buffs.Components;
using Core.Characteristics.Player.Components;
using Core.Collisions.Components;
using Core.Collisions.Strategies;
using Core.Collisions.Systems;
using Core.Extensions;
using Core.Extensions.Clear.Systems;
using Core.Movement.Gravity.Components;
using Core.Weapon.Components;
using Cysharp.Threading.Tasks;

namespace Core.Collisions
{
    public class CollisionsFeature : BaseMorpehFeature
    {
        protected async override UniTask InitializeSystems()
        {
            AddSystem(new TriggerSystem<ProjectileTag, ProjectileTag>(new DamageTargetStrategy()));
            AddSystem(new TriggerSystem<ProjectileTag, PlayerTag>(new DamageTargetStrategy()));
            AddSystem(new TriggerSystem<ProjectileTag, EnergyBuffTag>(new DamageTargetStrategy()));
            AddSystem(new TriggerSystem<GravityPoint, ProjectileTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<GravityPoint, PlayerTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<EnergyBuffTag, PlayerTag>(new ChargeTargetStrategy()));
            AddSystem(new TriggerSystem<PlayerTag, EnergyBuffTag>(new DestroyTargetStrategy()));
            AddSystem(new TriggerSystem<PlayerTag, PlayerTag>(new DamageTargetByHealthStrategy()));
            AddSystem(new DellHereFixedUpdateSystem<TriggerEnterRequest>());
        }
    }
}