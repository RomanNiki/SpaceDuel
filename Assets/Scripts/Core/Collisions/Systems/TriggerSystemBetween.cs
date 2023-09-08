﻿using Core.Collisions.Components;
using Core.Collisions.Strategies;

namespace Core.Collisions.Systems
{
    using Scellecs.Morpeh;
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class TriggerSystemBetween<TSenderTag, TTargetTag> : IFixedSystem
        where TSenderTag : struct, IComponent
        where TTargetTag : struct, IComponent
    {
        private readonly IEnterTriggerStrategy _strategy;
        private Filter _filter;
        private Stash<TriggerEnterRequest> _triggerEnterRequestPool;
        private Stash<TSenderTag> _senderTagPool;
        private Stash<TTargetTag> _targetTagPool;
        public World World { get; set; }

        public TriggerSystemBetween(IEnterTriggerStrategy strategy)
        {
            _strategy = strategy;
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<TriggerEnterRequest>().Build();
            _triggerEnterRequestPool = World.GetStash<TriggerEnterRequest>();
            _senderTagPool = World.GetStash<TSenderTag>();
            _targetTagPool = World.GetStash<TTargetTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
          
                ref var triggerRequest = ref _triggerEnterRequestPool.Get(entity);
                if (triggerRequest.Sender.IsNullOrDisposed() || triggerRequest.Target.IsNullOrDisposed())
                    continue;
                var sender = triggerRequest.Sender;
                var target = triggerRequest.Target;

                if (IsCollideBetween(sender, target) == false) continue;
                _strategy.OnEnter(World, sender, target);
                World.RemoveEntity(entity);
            }
        }

        private bool IsCollideBetween(Entity sender, Entity target)
        {
            return _senderTagPool.Has(sender) &&
                   _targetTagPool.Has(target);
        }

        public void Dispose()
        {
        }
    }
}