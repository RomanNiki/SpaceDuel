using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class SystemRegisterHandler
    {
        private readonly List<IEcsSystem> _runSystems;
        public readonly IReadOnlyList<IEcsSystem> RunSystems;

        public SystemRegisterHandler(
            [Inject(Source = InjectSources.Local)] List<IEcsSystem> runSystems)
        {
            _runSystems = runSystems;
            RunSystems = runSystems;
            foreach (var item in runSystems)
            {
                Debug.Log(item.GetType());
            }
        }
    }
}