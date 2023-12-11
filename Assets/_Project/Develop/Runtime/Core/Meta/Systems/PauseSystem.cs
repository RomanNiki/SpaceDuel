using _Project.Develop.Runtime.Core.Input.Components;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Pause;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Meta.Systems
{
    using Scellecs.Morpeh;

    public sealed class PauseSystem : ISystem
    {
        private readonly IPauseService _pauseService;
        private readonly IGame _game;
        private Filter _filter;
        
        public PauseSystem(IPauseService pauseService, IGame game)
        {
            _pauseService = pauseService;
            _game = game;
        }
        
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<InputMenuEvent>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_filter.IsNotEmpty())
            {
                if (_game.IsPlaying && _game.IsRestarting == false)
                {
                    _pauseService.SetPaused(_pauseService.IsPause == false);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}