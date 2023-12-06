using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Timers.Components;
using _Project.Develop.Runtime.Engine.Common;
using _Project.Develop.Runtime.Engine.Views.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Views.Effects.Systems
{
    public sealed class VisualizeLifeTimerByMaterialSystem<TTimerType> : ISystem
    where TTimerType : struct, IComponent
    {
        private Filter _filter;
        private Stash<MaterialIndicator> _materialTimerPool;
        private Stash<Timer<TTimerType>> _timerPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<MaterialIndicator>().With<Timer<TTimerType>>().Without<DeadTag>().Build();
            _materialTimerPool = World.GetStash<MaterialIndicator>();
            _timerPool = World.GetStash<Timer<TTimerType>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var materialIndicator = ref _materialTimerPool.Get(entity);
                ref var timer = ref _timerPool.Get(entity);
                var timeFactor = 1 - timer.TimeLeftSec / timer.InitialTimeSec;
                var targetColor = Color.Lerp(materialIndicator.StartColor, materialIndicator.EndColor, timeFactor);
                materialIndicator.Material.SetColor(Materials.EmissionColor, targetColor * Mathf.Pow(2, materialIndicator.Intencity));
                materialIndicator.Material.SetColor(Materials.BaseColor, targetColor);
            }
        }

        public void Dispose()
        {
        }
    }
}