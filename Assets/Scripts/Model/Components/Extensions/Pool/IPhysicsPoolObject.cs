using UnityEngine;

namespace Model.Components.Extensions.Pool
{
    public interface IPhysicsPoolObject : IPoolObject
    {
        Rigidbody2D Rigidbody2D { get; }
    }
}