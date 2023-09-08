using System;

namespace Modules.Pooling.Pool
{
    public interface IPoolItem : IDisposable
    {
        void OnDespawned();

        void OnSpawned(IPool pool);
    }
}