using Core.Weapon.Components;
using Engine.Views.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Sounds.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public class ShotSoundSystem : ISystem
    {
        private Filter _filter;
        private Stash<UnityComponent<AudioClip>> _audioClipPool;
        private Stash<Owner> _ownerPool;
        private Stash<UnityComponent<ShotAudioSource>> _soundSourcePool;
        private Stash<ShotMadeEvent> _shotMadeEventPool;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter.With<ShotMadeEvent>().Build();
            _audioClipPool = World.GetStash<UnityComponent<AudioClip>>();
            _ownerPool = World.GetStash<Owner>();
            _shotMadeEventPool = World.GetStash<ShotMadeEvent>();
            _soundSourcePool = World.GetStash<UnityComponent<ShotAudioSource>>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var eventEntity in _filter)
            {
                ref var weaponEntity = ref _shotMadeEventPool.Get(eventEntity).Weapon;
                if (weaponEntity.IsNullOrDisposed())
                {
                    continue;
                }
                ref var ownerEntity = ref _ownerPool.Get(weaponEntity);
                if (ownerEntity.Entity.IsNullOrDisposed())
                {
                    continue;
                }

                ref var audioClip = ref _audioClipPool.Get(weaponEntity);
                ref var audioSource = ref _soundSourcePool.Get(ownerEntity.Entity);
                audioSource.Value.ShotSound(audioClip.Value);
            }
        }

        public void Dispose()
        {
        }
    }
}