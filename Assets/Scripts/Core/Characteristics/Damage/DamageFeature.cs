using Core.Characteristics.Damage.Components;
using Core.Characteristics.Damage.Systems;
using Core.Effects.Components;
using Core.Meta.Components;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Characteristics.Damage
{
    public class DamageFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            RegisterEvent<DestroyedEvent<ExplosiveTag>>();
            RegisterEvent<DestroyedEvent<HitterTag>>();
            RegisterEvent<HealthChangedEvent>();
            RegisterEvent<GameOverEvent>();
            RegisterRequest<DamageRequest>();
            AddSystem(new DestroySystem());
            AddSystem(new InstantlyKillSystem());
            AddSystem(new KillWithoutHealthSystem());
            AddSystem(new DamageSystem());
            AddSystem(new CheckDeathSystem());
            AddSystem(new CheckOwnerDeathSystem());
            AddSystem(new PlayerDiedSystem());
            AddSystem(new DeathSystem());
            AddSystem(new SendDestroyedEventSystem<ExplosiveTag>());
            AddSystem(new SendDestroyedEventSystem<HitterTag>());
        }
    }
}