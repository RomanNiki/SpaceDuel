using _Project.Develop.Runtime.Core.Movement.Components.Events;
using _Project.Develop.Runtime.Engine.Common.Components;
using _Project.Develop.Runtime.Engine.Sounds;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.ECS.Sounds.Systems
{
    public class RotateSoundSystem : ISystem
    {
        private Stash<UnityComponent<MoveAudioSource>> _moveAudioSourcePool;
        private Filter _moveFilter;
        private Filter _stopFilter;
        public World World { get; set; }

        public void OnAwake()
        {
            _moveFilter = World.Filter.With<StartRotationEvent>().Build();
            _stopFilter = World.Filter.With<StopRotationEvent>().Build();
            _moveAudioSourcePool = World.GetStash<UnityComponent<MoveAudioSource>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _moveFilter)
            {
                if (_moveAudioSourcePool.Has(entity))
                {
                    _moveAudioSourcePool.Get(entity).Value.StartRotationSound();
                }
            }

            foreach (var entity in _stopFilter)
            {
                if (_moveAudioSourcePool.Has(entity))
                {
                    _moveAudioSourcePool.Get(entity).Value.StopRotationSound();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}