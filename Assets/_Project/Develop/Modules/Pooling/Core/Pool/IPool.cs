namespace _Project.Develop.Modules.Pooling.Core.Pool
{
    public interface IPool
    {
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