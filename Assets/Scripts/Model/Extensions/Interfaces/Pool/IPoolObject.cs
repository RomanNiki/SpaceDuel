using UnityEngine;

namespace Model.Extensions.Interfaces.Pool
{
    public interface IPoolObject
    {
        void PoolRecycle();
        public Transform Transform { get; }
    }
}