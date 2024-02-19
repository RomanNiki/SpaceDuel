using _Project.Develop.Runtime.Engine.Common.Components;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using _Project.Develop.Runtime.Engine.Sounds;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Sounds
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