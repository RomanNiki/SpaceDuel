using Core.Movement.Components.Events;
using Engine.Views.Components;
using Scellecs.Morpeh;

namespace Engine.Sounds.Systems
{
    public class AccelerationSoundSystem : ISystem
    {
        private Filter _moveFilter;
        private Filter _stopFilter;
        private Stash<UnityComponent<MoveAudioSource>> _moveAudioSourcePool;
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