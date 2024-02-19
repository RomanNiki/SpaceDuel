using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Systems
{
    public class EnergyBlockSystem : ISystem
    {
        private Filter _filter;
        private Stash<Energy> _energyPool;
        
        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter.With<Energy>().Build();
            _energyPool = World.GetStash<Energy>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var energy = ref _energyPool.Get(entity);
                energy.HasEnergy = energy.Value > energy.MinValue;
            }
        }
        
        public void Dispose()
        {
        }
    }
}