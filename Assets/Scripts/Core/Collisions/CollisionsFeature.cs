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
            AddSystem(new TriggerSystemBetween<ProjectileTag, ProjectileTag>(new DamagerTriggerStrategy()));
            AddSystem(new TriggerSystemBetween<ProjectileTag, PlayerTag>(new DamagerTriggerStrategy()));
            AddSystem(new TriggerSystemBetween<GravityPoint, ProjectileTag>(new GravityPointTriggerStrategy()));
            AddSystem(new TriggerSystemBetween<GravityPoint, PlayerTag>(new GravityPointTriggerStrategy()));
            AddSystem(new TriggerSystemBetween<PlayerTag, PlayerTag>(new PlayersTriggerStrategy()));
            AddSystem(new DellHereFixedUpdateSystem<TriggerEnterRequest>());
        }
    }
}