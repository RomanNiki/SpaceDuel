using UnityEngine;

namespace Model.Components.Extensions.Pool
{
    public interface IPoolObject
    {
        void PoolRecycle();
        Rigidbody2D Rigidbody2D { get; }
        public Transform Transform { get; }
    }
}