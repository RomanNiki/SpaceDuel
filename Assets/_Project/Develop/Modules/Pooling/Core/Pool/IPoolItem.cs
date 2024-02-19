using System;

namespace _Project.Develop.Modules.Pooling.Core.Pool
{
    public interface IPoolItem : IDisposable
    {
        void OnDespawned();

        void OnSpawned(IPool pool);
    }
}