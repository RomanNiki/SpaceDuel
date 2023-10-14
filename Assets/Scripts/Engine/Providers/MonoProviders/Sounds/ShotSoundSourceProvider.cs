using Engine.Providers.MonoProviders.Base;
using Engine.Sounds;
using Engine.Views.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers.MonoProviders.Sounds
{
    public class ShotSoundSourceProvider : MonoProviderBase
    {
        [SerializeField] private ShotAudioSource _shotAudioSource;

        public override void Resolve(World world, Entity entity)
        {
            var stash = world.GetStash<UnityComponent<ShotAudioSource>>();
            stash.Set(entity, new UnityComponent<ShotAudioSource> { Value = _shotAudioSource });
        }
    }
}