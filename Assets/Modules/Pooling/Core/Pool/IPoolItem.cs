using System;

namespace Modules.Pooling.Core.Pool
{
    public interface IPoolItem : IDisposable
    {
        void OnDespawned();

        void OnSpawned(IPool pool);
    }
}