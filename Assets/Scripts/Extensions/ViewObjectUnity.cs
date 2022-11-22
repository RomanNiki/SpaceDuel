using Components;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Extensions
{
    public class ViewObjectUnity : IViewObject
    {
        public Vector2 Position
        {
            get => _rigidbody2D.position;
            set => _rigidbody2D.position = value;
        }

        public float Rotation
        {
            get => _rigidbody2D.rotation;
            set => _rigidbody2D.rotation = value;
        }

        public Vector2 Velocity { get => _rigidbody2D.velocity; set => _rigidbody2D.velocity = value ; }
        [CanBeNull] private IMemoryPool _poolObject;
        [NotNull] private readonly Rigidbody2D _rigidbody2D;

        public ViewObjectUnity([NotNull] Rigidbody2D rigidbody2D, IMemoryPool pool = null)
        {
            _rigidbody2D = rigidbody2D;
            _poolObject = pool;
        }

        public void MoveTo(in Vector2 vector2)
        {
            _rigidbody2D.MovePosition(vector2);
        }

        public void SetPool(IMemoryPool pool)
        {
            _poolObject = pool;
        }

        public void Destroy()
        {
            if (_poolObject != null)
            {
                _poolObject.Despawn(_rigidbody2D.gameObject);
            }
            else
            {
                Object.Destroy(_rigidbody2D.gameObject);
            }
        }
    }
}