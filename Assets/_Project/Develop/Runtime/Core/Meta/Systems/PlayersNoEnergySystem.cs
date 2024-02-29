using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Characteristics.EnergyLimits.Components;
using _Project.Develop.Runtime.Core.Common;
using _Project.Develop.Runtime.Core.Player.Components;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Pause;
using _Project.Develop.Runtime.Core.Services.Time;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Meta.Systems
{
    public class PlayersNoEnergySystem : ISystem, IPauseHandler
    {
        private readonly ITimeScale _timeScale;
        private readonly IGame _game;
        private readonly IPauseService _pauseService;
        private Filter _filter;
        private bool _paused;
        private Stash<Energy> _energyPool;
        
        public PlayersNoEnergySystem(ITimeScale timeScale, IGame game, IPauseService pauseService)
        {
            _timeScale = timeScale;
            _game = game;
            _pauseService = pauseService;
            _pauseService.AddPauseHandler(this);
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<PlayerTag>().With<Energy>().Without<DeadTag>().Build();
            _energyPool = World.GetStash<Energy>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_game.IsRestarting || _game.IsPlaying == false || _paused)
                return;

            var count = 0;
            foreach (var entity in _filter)
            {
                if (_energyPool.Get(entity).HasEnergy == false) count++;
            }

            if (count == 2)
            {
                if (Mathf.Approximately(_timeScale.TimeScale, GameConfig.AcceleratedTimeScale) == false)
                {
                    _timeScale.SetTimeScale(GameConfig.AcceleratedTimeScale);
                }
                
                return;
            }

            if (Mathf.Approximately(_timeScale.TimeScale, GameConfig.DefaultTimeScale))
                return;
            _timeScale.Reset();
        }

        public void Dispose()
        {
            _pauseService.RemovePauseHandler(this);
        }

        public void SetPaused(bool isPaused)
        {
            _paused = isPaused;
        }
    }
}