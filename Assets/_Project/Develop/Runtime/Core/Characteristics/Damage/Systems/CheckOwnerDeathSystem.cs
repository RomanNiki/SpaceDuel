﻿using _Project.Develop.Runtime.Core.Characteristics.Damage.Components;
using _Project.Develop.Runtime.Core.Weapon.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Characteristics.Damage.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class CheckOwnerDeathSystem : ISystem
    {
        private Filter _filter;
        private Stash<Owner> _ownerPool;
        private Stash<DeadTag> _deadTag;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<Owner>().Build();
            _ownerPool = World.GetStash<Owner>();
            _deadTag = World.GetStash<DeadTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var owner = ref _ownerPool.Get(entity).Entity;
                if (owner.IsNullOrDisposed() || _deadTag.Has(owner))
                {
                    _deadTag.Set(entity);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}