using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Time;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

namespace _Project.Develop.Runtime
{
    public class WorldUpdater : MonoBehaviour
    {
        private IGame _game;
        private ITimeScale _timeScale;

        [Inject]
        private void Init(IGame game, ITimeScale timeScale)
        {
            _game = game;
            _timeScale = timeScale;
        }
        
        private void Update()
        {
            if (_game is { IsPlaying: true })
            {
                WorldExtensions.GlobalUpdate(Time.deltaTime * _timeScale.TimeScale);
            }
        }

        private void FixedUpdate()
        {
            if (_game is { IsPlaying: true })
            {
                WorldExtensions.GlobalFixedUpdate(Time.fixedDeltaTime * _timeScale.TimeScale);
            }
        }

        private void LateUpdate()
        {
            if (_game is { IsPlaying: true })
            {
                WorldExtensions.GlobalLateUpdate(Time.deltaTime * _timeScale.TimeScale);
            }
        }
    }
}