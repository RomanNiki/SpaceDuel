using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Characteristics.Damage.Systems;
using _Project.Develop.Runtime.Core.Meta.Components;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Characteristics.Damage
{
    public sealed class DamageFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            RegisterEvent<HealthChangedEvent>();
            RegisterEvent<GameOverEvent>();
            RegisterEvent<DestroyEvent>();
            AddSystem(new DestroySystem());
            AddSystem(new InstantlyKillSystem());
            AddSystem(new DamageSystem());
            AddSystem(new CheckDeathSystem());
            AddSystem(new CheckOwnerDeathSystem());
            AddSystem(new PlayerDiedSystem());
            AddSystem(new DeathSystem());
            RegisterRequest<DamageRequest>();
            RegisterRequest<KillRequest>();
        }
    }
}