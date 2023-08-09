using System.Collections.Generic;
using Leopotam.Ecs;
using Zenject;

namespace Extensions
{
    public sealed class SystemRegisterHandler
    {
        public readonly IReadOnlyList<IEcsSystem> RunSystems;
        public readonly IReadOnlyList<IEcsSystem> FixedRunSystems;

        [Inject]
        public SystemRegisterHandler(
            [Inject(Id = SystemsEnum.Run)] List<IEcsSystem> runSystems,
            [Inject(Id = SystemsEnum.FixedRun)] List<IEcsSystem> fixedRunSystems)
        {
            RunSystems = runSystems;
            FixedRunSystems = fixedRunSystems;
        }
        
        
    }

    public enum SystemsEnum
    {
        Run,
        FixedRun,
        LateRun
    }
}