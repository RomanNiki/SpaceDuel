using _Project.Develop.Runtime.Core.Timers.Components;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Timers
{
    public class LifeTimerMonoProvider : MonoProvider<Timer<LifeTimer>>
    {
        protected override void OnResolve(World world, Entity entity)
        {
            world.GetStash<DieWithoutLifeTimerTag>().Set(entity);
        }
    }
}