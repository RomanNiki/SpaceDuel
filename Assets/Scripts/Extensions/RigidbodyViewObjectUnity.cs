using JetBrains.Annotations;
using Model.Components;
using Model.Components.Extensions.Pool;
using UnityEngine;

namespace Extensions
{
    public class RigidbodyViewObjectUnity : IViewObject
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
        
        [CanBeNull] private readonly IPoolObject _poolObject;
        [NotNull] private readonly Rigidbody2D _rigidbody2D;

        public RigidbodyViewObjectUnity([NotNull] Rigidbody2D rigidbody2D, IPoolObject poolObject = null)
        {
            _rigidbody2D = rigidbody2D;
            _poolObject = poolObject;
        }

        public void MoveTo(in Vector2 vector2)
        {
            _rigidbody2D.MovePosition(vector2);
        }

        public void Destroy()
        {
            if (_poolObject != null)
            {
                _poolObject.PoolRecycle();
            }
            else
            {
                Object.Destroy(_rigidbody2D.gameObject);
            }
        }
    }
}