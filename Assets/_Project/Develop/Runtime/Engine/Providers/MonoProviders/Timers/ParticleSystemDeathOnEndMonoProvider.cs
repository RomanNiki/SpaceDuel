﻿using _Project.Develop.Runtime.Core.Timers.Components;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Timers
{
    public class ParticleSystemDeathOnEndMonoProvider : MonoProviderBase
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public override void Resolve(World world, Entity entity)
        {
            var dieWithoutTimerTagStash = world.GetStash<DieWithoutLifeTimerTag>();
            dieWithoutTimerTagStash.Set(entity);
            var lifeCycleTimerPool = world.GetStash<Timer<LifeTimer>>();
            lifeCycleTimerPool.Set(entity, new Timer<LifeTimer>(_particleSystem.main.duration));
        }
    }
}