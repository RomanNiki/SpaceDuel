using Core.Characteristics.Damage.Components;
using Core.Characteristics.Damage.Systems;
using Core.Extensions;
using Core.Extensions.Clear.Systems;
using Cysharp.Threading.Tasks;

namespace Core.Characteristics.Damage
{
    public class DamageFeature : BaseMorpehFeature
    {
        protected async override UniTask InitializeSystems()
        {
            AddSystem(new DellHereUpdateSystem<HealthChangedEvent>());
            AddSystem(new InstantlyKillSystem());
            AddSystem(new KillWithoutHealthSystem());
            AddSystem(new DamageSystem());
            AddSystem(new CheckDeathSystem());
            AddSystem(new CheckOwnerDeathSystem());
            AddSystem(new DeathSystem());
            AddSystem(new DestroySystem());
        }
    }
}