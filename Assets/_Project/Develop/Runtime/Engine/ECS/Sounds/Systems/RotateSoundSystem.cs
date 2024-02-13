using System.Collections.Generic;
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
        private readonly HashSet<Entity> _rotatingHashSet = new();
        private Stash<StartRotationEvent> _startRotationEventPool;
        private Stash<StopRotationEvent> _stopRotationEventPool;

        public void OnAwake()
        {
            _moveFilter = World.Filter.With<StartRotationEvent>().Build();
            _stopFilter = World.Filter.With<StopRotationEvent>().Build();
            _startRotationEventPool = World.GetStash<StartRotationEvent>();
            _stopRotationEventPool = World.GetStash<StopRotationEvent>();
            _moveAudioSourcePool = World.GetStash<UnityComponent<MoveAudioSource>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _moveFilter)
            {
                ref var playerEntity = ref _startRotationEventPool.Get(entity).Entity;
                if (playerEntity.IsNullOrDisposed()) continue;
                if (_moveAudioSourcePool.Has(playerEntity))
                {
                    _rotatingHashSet.Add(playerEntity);
                }
            }

            foreach (var entity in _stopFilter)
            {
                ref var playerEntity = ref _stopRotationEventPool.Get(entity).Entity;
                _rotatingHashSet.Remove(playerEntity);
                if (playerEntity.IsNullOrDisposed()) continue;
                _moveAudioSourcePool.Get(playerEntity).Value.StopRotatingSound();
            }

            ProcessRotateSound();

            _rotatingHashSet.RemoveWhere(x => x.IsNullOrDisposed());
        }

        private void ProcessRotateSound()
        {
            foreach (var entity in _rotatingHashSet)
            {
                if (entity.IsNullOrDisposed())
                {
                    continue;
                }

                _moveAudioSourcePool.Get(entity).Value.StartRotatingSound();
            }
        }

        public void Dispose()
        {
        }
    }
}