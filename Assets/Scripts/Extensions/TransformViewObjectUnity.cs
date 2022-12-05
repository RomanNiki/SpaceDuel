using JetBrains.Annotations;
using Model.Components;
using Model.Components.Extensions.Pool;
using UnityEngine;

namespace Extensions
{
    public class TransformViewObjectUnity : IViewObject
    {
        public Vector2 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        public float Rotation
        {
            get => _transform.rotation.z;
            set
            {
                var rotation = _transform.rotation;
                rotation.z = value;
                _transform.rotation = rotation;
            }
        }

        [CanBeNull] private readonly IPoolObject _poolObject;
        [NotNull] private readonly Transform _transform;

        public TransformViewObjectUnity([NotNull] Transform transform, IPoolObject poolObject = null)
        {
            _transform = transform;
            _poolObject = poolObject;
        }

        public void MoveTo(in Vector2 vector2)
        {
            _transform.Translate(vector2 - (Vector2) _transform.position);
        }

        public void Destroy()
        {
            if (_poolObject != null)
            {
                _poolObject.PoolRecycle();
            }
            else
            {
                Object.Destroy(_transform.gameObject);
            }
        }
    }
}