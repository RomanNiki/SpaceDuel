using Core.Characteristics.Damage.Components;
using Core.Characteristics.Damage.Systems;
using Core.Effects.Components;
using Core.Extensions;
using Core.Extensions.Clear.Systems;
using Cysharp.Threading.Tasks;

namespace Core.Characteristics.Damage
{
    public class DamageFeature : BaseMorpehFeature
    {
        protected async override UniTask InitializeSystems()
        {
            AddSystem(new DellHereUpdateSystem<DestroyedEvent<ExplosiveTag>>());
            AddSystem(new DellHereUpdateSystem<HealthChangedEvent>());
            AddSystem(new InstantlyKillSystem());
            AddSystem(new KillWithoutHealthSystem());
            AddSystem(new DamageSystem());
            AddSystem(new CheckDeathSystem());
            AddSystem(new CheckOwnerDeathSystem());
            AddSystem(new DeathSystem());
            AddSystem(new SendDestroyedEventSystem<ExplosiveTag>());
            AddSystem(new DestroySystem());
        }
    }
}