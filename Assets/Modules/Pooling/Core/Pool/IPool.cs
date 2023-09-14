using UnityEngine;

namespace Modules.Pooling.Core.Pool
{
    public interface IPool
    {
        void Resize(int desiredPoolSize);
        void Despawn(object obj);
    }

    public interface IDespawnablePool<TComponent> : IPool
    {
        void Despawn(TComponent item);
    }
    
    public interface IPool<TComponent> : IDespawnablePool<TComponent>
    {
        TComponent Spawn(Vector3 position = new Vector3(), float rotation = 0);
    }
}