using Cysharp.Threading.Tasks;

namespace Modules.Pooling.Core.Pool
{
    public interface IPool : ILoadingResource
    {
        UniTask Resize(int desiredPoolSize);
        void Despawn(object obj);
    }

    public interface IDespawnablePool<TComponent> : IPool
    {
        UniTaskVoid Despawn(TComponent item);
    }

    public interface IPool<TComponent> : IDespawnablePool<TComponent>
    {
        UniTask<TComponent> Spawn();
    }
}