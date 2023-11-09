using Core.Characteristics.Damage.Components;
using Core.Extensions;
using Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;

namespace Engine.Providers.MonoProviders.Damagers
{
    public class HealthMonoProvider : MonoProvider<Health>
    {
        protected override void OnResolve(World world, Entity entity)
        {
            world.SendMessage(new HealthChangedEvent { Entity = entity });
        }
    }
}