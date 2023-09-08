namespace Modules.Pooling.Pool
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
        TComponent Spawn();
    }
}