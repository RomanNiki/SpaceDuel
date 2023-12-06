using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Timers.Components;
using _Project.Develop.Runtime.Engine.Common;
using _Project.Develop.Runtime.Engine.Views.Components;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Views.Effects.Systems
{
    using Scellecs.Morpeh;

    public sealed class VisualizeEnergyByMaterialSystem : ISystem
    {
        private Filter _filter;
        private Stash<MaterialIndicator> _materialTimerPool;
        private Stash<Energy> _energyPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<MaterialIndicator>().With<Energy>().Without<Timer<InvisibleTimer>>().Without<DeadTag>().Build();
            _materialTimerPool = World.GetStash<MaterialIndicator>();
            _energyPool = World.GetStash<Energy>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var materialIndicator = ref _materialTimerPool.Get(entity);
                ref var energy = ref _energyPool.Get(entity);
                var delta = energy.Value / energy.BaseValue;
                var oneMinusDelta = 1 - delta;
                var targetColor = Color.Lerp(materialIndicator.EndColor, materialIndicator.StartColor * 0.5f, oneMinusDelta);
                materialIndicator.Material.SetColor(Materials.EmissionColor, targetColor * Mathf.Pow(2, materialIndicator.Intencity));
                materialIndicator.Material.SetColor(Materials.BaseColor, targetColor);
            }
        }

        public void Dispose()
        {
        }
    }
}