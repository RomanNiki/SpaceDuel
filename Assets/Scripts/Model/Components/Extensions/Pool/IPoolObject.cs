using UnityEngine;

namespace Model.Components.Extensions.Pool
{
    public interface IPoolObject
    {
        void PoolRecycle();
        public Transform Transform { get; }
    }
}