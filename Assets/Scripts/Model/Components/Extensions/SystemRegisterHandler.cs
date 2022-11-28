using System.Collections.Generic;
using Leopotam.Ecs;
using Zenject;

namespace Model.Components.Extensions
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
        }
    }

    public enum SystemsEnum
    {
        Run,
        FixedRun,
        LateRun
    }
}