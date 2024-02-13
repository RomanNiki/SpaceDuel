using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Player.Components;
using _Project.Develop.Runtime.Core.Services.Time;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Meta.Systems
{
    public class PlayersNoEnergySystem : ISystem
    {
        private readonly ITimeScale _timeScale;
        private Filter _filter;
        private bool _paused;
        private Stash<Energy> _energyPool;
        private const float DefaultTimeScale = 1f;
        private const float AcceleratedTimeScale = 3f;

        public PlayersNoEnergySystem(ITimeScale timeScale)
        {
            _timeScale = timeScale;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<PlayerTag>().With<Energy>().Without<DeadTag>().Build();
            _energyPool = World.GetStash<Energy>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (Mathf.Approximately(_timeScale.TimeScale, 0f))
            {
                return;
            }

            var count = 0;
            foreach (var entity in _filter)
            {
                if (_energyPool.Get(entity).HasEnergy == false)
                {
                    count++;
                }
            }

            if (count == 2)
            {
                if (Mathf.Approximately(_timeScale.TimeScale, AcceleratedTimeScale))
                    return;

                _timeScale.SetTimeScale(AcceleratedTimeScale);
                return;
            }

            if (Mathf.Approximately(_timeScale.TimeScale, DefaultTimeScale))
                return;
            _timeScale.SetTimeScale(DefaultTimeScale);
        }

        public void Dispose()
        {
        }
    }
}