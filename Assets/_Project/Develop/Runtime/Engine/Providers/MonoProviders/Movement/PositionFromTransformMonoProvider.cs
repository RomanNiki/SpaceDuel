using _Project.Develop.Runtime.Core.Movement.Components;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Movement
{
    public class PositionFromTransformMonoProvider : MonoProviderBase
    {
        [SerializeField] private Transform _transform;

        private void OnValidate()
        {
            _transform ??= transform;
        }

        public override void Resolve(World world, Entity entity)
        {
            var stash = world.GetStash<Position>();
            stash.Set(entity, new Position { Value = _transform.position });
        }
    }
}