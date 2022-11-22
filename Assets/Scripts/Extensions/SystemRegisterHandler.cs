using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Extensions
{
    public sealed class SystemRegisterHandler
    {
        public readonly IReadOnlyList<IEcsSystem> RunSystems;
        public readonly IReadOnlyList<IEcsSystem> FixedRunSystems;

        public SystemRegisterHandler(
            [Inject(Source = InjectSources.Local, Id = SystemsEnum.Run)] List<IEcsSystem> runSystems,[Inject(Source = InjectSources.Local, Id = SystemsEnum.FixedRun)] List<IEcsSystem> fixedRunSystems)
        {
            RunSystems = runSystems;
            FixedRunSystems = fixedRunSystems;
            foreach (var item in runSystems)
            {
                Debug.Log(item.GetType());
            }foreach (var item in fixedRunSystems)
            {
                Debug.Log(item.GetType());
            }
        }
    }

    public enum SystemsEnum
    {
        Run,
        FixedRun,
        LateRun
    }
}