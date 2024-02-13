using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Sounds.AudioStateMachine
{
    public sealed class SpaceShipAudioEngine : AudioEngineStateMachine
    {
        [SerializeField] private AudioPauseHandler _pauseHandler;
        protected override bool IsPaused => _pauseHandler.Paused;
    }
}