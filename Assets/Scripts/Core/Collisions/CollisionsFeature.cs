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
            AddSystem(new TriggerSystem<ProjectileTag, ProjectileTag>(new DamagerTriggerStrategy()));
            AddSystem(new TriggerSystem<ProjectileTag, PlayerTag>(new DamagerTriggerStrategy()));
            AddSystem(new TriggerSystem<GravityPoint, ProjectileTag>(new GravityPointTriggerStrategy()));
            AddSystem(new TriggerSystem<GravityPoint, PlayerTag>(new GravityPointTriggerStrategy()));
            AddSystem(new TriggerSystem<PlayerTag, PlayerTag>(new PlayersTriggerStrategy()));
            AddSystem(new DellHereFixedUpdateSystem<TriggerEnterRequest>());
        }
    }
}