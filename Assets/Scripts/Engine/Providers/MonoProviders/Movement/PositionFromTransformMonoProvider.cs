using Core.Movement.Components;
using Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers.MonoProviders.Movement
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