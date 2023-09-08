using Core.Characteristics.EnergyLimits.Components;
using Core.Movement.Gravity.Components;

namespace Core.Movement.Gravity.Systems
{
    using Scellecs.Morpeh;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class NoEnergyGravityResistSystem : ISystem
    {
        private Filter _filter;
        private Stash<GravityResistTag> _gravityResistPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<GravityResistTag>().With<NoEnergyBlock>().Build();
            _gravityResistPool = World.GetStash<GravityResistTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                _gravityResistPool.Remove(entity);
            }
        }

        public void Dispose()
        {
        }
    }
}