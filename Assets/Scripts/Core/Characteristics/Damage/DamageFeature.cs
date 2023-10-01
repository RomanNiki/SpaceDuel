using Core.Characteristics.Damage.Components;
using Core.Characteristics.Damage.Systems;
using Core.Effects.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Characteristics.Damage
{
    public class DamageFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            RegisterEvent<DestroyedEvent<ExplosiveTag>>();
            RegisterEvent<DestroyedEvent<BulletTag>>();
            RegisterEvent<HealthChangedEvent>();
            AddSystem(new InstantlyKillSystem());
            AddSystem(new KillWithoutHealthSystem());
            AddSystem(new DamageSystem());
            AddSystem(new CheckDeathSystem());
            AddSystem(new CheckOwnerDeathSystem());
            AddSystem(new DeathSystem());
            AddSystem(new SendDestroyedEventSystem<ExplosiveTag>());
            AddSystem(new SendDestroyedEventSystem<BulletTag>());
            AddSystem(new DestroySystem());
        }
    }
}