using _Project.Develop.Runtime.Core.Buffs.Components;
using _Project.Develop.Runtime.Core.Timers.Components;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Buffs
{
    public class BuffSpawnerMonoProvider : MonoProviderBase
    {
        [SerializeField] private BuffSpawner _buffSpawner;

        public override void Resolve(World world, Entity entity)
        {
            world.GetStash<BuffSpawner>().Set(entity, _buffSpawner);
            world.GetStash<Timer<TimerBetweenSpawn>>()
                .Set(entity, new Timer<TimerBetweenSpawn>(_buffSpawner.SpawnIntervalSec));
        }
    }
}