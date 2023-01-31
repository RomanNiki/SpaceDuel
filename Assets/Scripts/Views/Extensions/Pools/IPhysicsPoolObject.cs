using Model.Extensions.Interfaces.Pool;
using UnityEngine;

namespace Views.Extensions.Pools
{
    public interface IPhysicsPoolObject : IPoolObject
    {
        Rigidbody2D Rigidbody2D { get; }
    }
}