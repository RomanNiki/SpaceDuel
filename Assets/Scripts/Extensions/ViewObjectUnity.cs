using System;
using JetBrains.Annotations;
using Model.Components;
using Model.Extensions.Interfaces.Pool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Extensions
{
    public class ViewObjectUnity : IViewObject
    {
        public Vector2 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        public float Rotation
        {
            get => _transform.eulerAngles.z;
            set
            {
                var angles = _transform.eulerAngles;
                angles.z = value;
                _transform.eulerAngles = angles;
            }
        }

        [NotNull] private readonly Transform _transform;
        [CanBeNull] private readonly Rigidbody2D _rigidbody2D;
        
        [CanBeNull] private readonly IPoolObject _poolObject;

        public ViewObjectUnity([NotNull] Transform transform, [NotNull] Rigidbody2D rigidbody2D,
            IPoolObject poolObject = null) : this(transform, poolObject)
        {
            _rigidbody2D = rigidbody2D;
        }

        public ViewObjectUnity([NotNull] Transform transform, IPoolObject poolObject = null)
        {
            _transform = transform ? transform : throw new ArgumentNullException(nameof(transform));
            _poolObject = poolObject;
        }

        public void MoveTo(in Vector2 vector2)
        {
            if (_rigidbody2D != null)
            {
                _rigidbody2D.MovePosition(vector2);
            }
            else
            {
                _transform.Translate(vector2 - (Vector2) _transform.position);
            }
        }

        public void RotateTo(float rotation)
        {
            var angles = _transform.eulerAngles;
            angles.z = rotation;
            _transform.eulerAngles = angles;
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