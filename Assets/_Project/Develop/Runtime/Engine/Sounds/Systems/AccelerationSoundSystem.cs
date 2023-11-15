using _Project.Develop.Runtime.Core.Movement.Components.Events;
using _Project.Develop.Runtime.Engine.Views.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.Sounds.Systems
{
    public class AccelerationSoundSystem : ISystem
    {
        private Stash<UnityComponent<MoveAudioSource>> _moveAudioSourcePool;
        private Filter _moveFilter;
        private Filter _stopFilter;
        public World World { get; set; }

        public void OnAwake()
        {
            _moveFilter = World.Filter.With<StartAccelerationEvent>().Build();
            _stopFilter = World.Filter.With<StopAccelerationEvent>().Build();
            _moveAudioSourcePool = World.GetStash<UnityComponent<MoveAudioSource>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _moveFilter)
            {
                if (_moveAudioSourcePool.Has(entity))
                {
                    _moveAudioSourcePool.Get(entity).Value.StartAccelerateSound();
                }
            } 
            
            foreach (var entity in _stopFilter)
            {
                if (_moveAudioSourcePool.Has(entity))
                {
                    _moveAudioSourcePool.Get(entity).Value.StartAccelerateSound();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}