using Core.Timers.Components;
using Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers.MonoProviders.Timers
{
    public class ParticleSystemDeathOnEndMonoProvider : MonoProviderBase
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public override void Resolve(World world, Entity entity)
        {
            var dieWithoutTimerTagStash = world.GetStash<DieWithoutLifeTimerTag>();
            dieWithoutTimerTagStash.Set(entity);
            var lifeCycleTimer = world.GetStash<Timer<LifeTimer>>();
            lifeCycleTimer.Set(entity, new Timer<LifeTimer> { TimeLeftSec = _particleSystem.main.duration + 0.1f });
        }
    }
}