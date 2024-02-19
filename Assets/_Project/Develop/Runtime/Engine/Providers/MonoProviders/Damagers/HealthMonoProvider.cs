using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Damagers
{
    public class HealthMonoProvider : MonoProvider<Health>
    {
        protected override void OnResolve(World world, Entity entity)
        {
            world.SendMessage(new HealthChangedEvent { Entity = entity });
        }
    }
}