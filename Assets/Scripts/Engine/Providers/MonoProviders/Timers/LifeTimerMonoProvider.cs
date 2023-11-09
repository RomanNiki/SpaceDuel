using Core.Timers.Components;
using Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;

namespace Engine.Providers.MonoProviders.Timers
{
    public class LifeTimerMonoProvider : MonoProvider<Timer<LifeTimer>>
    {
        protected override void OnResolve(World world, Entity entity)
        {
            world.GetStash<DieWithoutLifeTimerTag>().Set(entity);
        }
    }
}