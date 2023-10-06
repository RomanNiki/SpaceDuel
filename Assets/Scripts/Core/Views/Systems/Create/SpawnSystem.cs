using Core.Services;
using Core.Views.Components;
using Scellecs.Morpeh;

namespace Core.Views.Systems.Create
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class SpawnSystem : SpawnSystemBase<SpawnRequest>
    {
        private readonly IAssets _pools;
    
        public SpawnSystem(IAssets pools) => _pools = pools;

        protected override Entity CreateView(SpawnRequest spawnRequest) {
            _pools.Create(spawnRequest, World);
            return spawnRequest.Entity;
        }
    }
}