using Entities;
using JetBrains.Annotations;
using UnityEngine;
using Views.Moveable;

namespace Views
{
    public sealed class UnityViewObject : ViewObjectBase
    {
        [NotNull] private readonly Transform _transform;
        [NotNull] private readonly EntityProvider _provider;
        private readonly IMoveStrategy _moveStrategy;

        public UnityViewObject(Transform transform, IMoveStrategy moveStrategy, [NotNull] EntityProvider provider)
        {
            _moveStrategy = moveStrategy;
            _transform = transform;
            _provider = provider;
        }

        public override void MoveTo(Vector2 position)
        {
            _moveStrategy?.MoveTo(position);
        }

        public override void RotateTo(float rotation)
        {
            var angles = _transform.eulerAngles;
            angles.z = rotation;
            _transform.eulerAngles = angles;
        }

        public override void Dispose()
        {
            _provider.Dispose();
        }
    }
}