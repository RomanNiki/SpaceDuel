using UnityEngine;

namespace Model.Components.Extensions.Interfaces.Pool
{
    public interface IPoolObject
    {
        void PoolRecycle();
        public Transform Transform { get; }
    }
}