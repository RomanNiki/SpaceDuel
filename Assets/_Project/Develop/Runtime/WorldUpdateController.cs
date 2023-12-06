using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Services.Time;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

namespace _Project.Develop.Runtime
{
    public class WorldUpdateController : MonoBehaviour
    {
        private World _world;
        private IGame _game;
        private ITimeScale _timeScale;

        [Inject]
        private void Init(IGame game, ITimeScale timeScale)
        {
            _game = game;
            _timeScale = timeScale;
        }

        private void Awake()
        {
            _world = World.Default;
            if (_world != null)
                _world.UpdateByUnity = false;
        }

        private void Update()
        {
            if (_game is { IsPlaying: true })
            {
                _world?.Update(Time.deltaTime * _timeScale.TimeScale);
            }
        }

        private void FixedUpdate()
        {
            if (_game is { IsPlaying: true })
            {
                _world?.FixedUpdate(Time.fixedDeltaTime * _timeScale.TimeScale);
            }
        }

        private void LateUpdate()
        {
            if (_game is { IsPlaying: true })
            {
                var deltaTime = Time.deltaTime * _timeScale.TimeScale;
                _world?.LateUpdate(deltaTime);
                _world?.CleanupUpdate(deltaTime);
            }
        }
    }
}