using System.Collections.Generic;
using _Project.Develop.Runtime.Core.Movement.Components.Events;
using _Project.Develop.Runtime.Engine.Common.Components;
using _Project.Develop.Runtime.Engine.Sounds;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.ECS.Sounds.Systems
{
    public class AccelerationSoundSystem : ISystem
    {
        private Stash<UnityComponent<MoveAudioSource>> _moveAudioSourcePool;
        private Filter _moveFilter;
        private Filter _stopFilter;
        public World World { get; set; }
        private readonly HashSet<Entity> _acceleratingHashSet = new();
        private Stash<StartAccelerationEvent> _startAccelerationEventPool;
        private Stash<StopAccelerationEvent> _stopAccelerationEventPool;

        public void OnAwake()
        {
            _moveFilter = World.Filter.With<StartAccelerationEvent>().Build();
            _stopFilter = World.Filter.With<StopAccelerationEvent>().Build();
            _startAccelerationEventPool = World.GetStash<StartAccelerationEvent>();
            _stopAccelerationEventPool = World.GetStash<StopAccelerationEvent>();
            _moveAudioSourcePool = World.GetStash<UnityComponent<MoveAudioSource>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _moveFilter)
            {
                ref var playerEntity = ref _startAccelerationEventPool.Get(entity).Entity;
                if (playerEntity.IsNullOrDisposed()) continue;
                if (_moveAudioSourcePool.Has(playerEntity))
                {
                    _acceleratingHashSet.Add(playerEntity);
                }
            }

            foreach (var entity in _stopFilter)
            {
                ref var playerEntity = ref _stopAccelerationEventPool.Get(entity).Entity;
                _acceleratingHashSet.Remove(playerEntity);
                if (playerEntity.IsNullOrDisposed() == false)
                {
                    _moveAudioSourcePool.Get(playerEntity).Value.StopAcceleratingSound();
                }
            }
            
            ProcessAccelerateSound();

            _acceleratingHashSet.RemoveWhere(x => x.IsNullOrDisposed());
        }

        private void ProcessAccelerateSound()
        {
            foreach (var entity in _acceleratingHashSet)
            {
                if (entity.IsNullOrDisposed())
                {
                    continue;
                }

                _moveAudioSourcePool.Get(entity).Value.StartAcceleratingSound();
            }
        }

        public void Dispose()
        {
        }
    }
}