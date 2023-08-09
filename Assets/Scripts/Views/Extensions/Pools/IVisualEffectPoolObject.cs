using Model.Extensions.Interfaces.Pool;

namespace Views.Extensions.Pools
{
    public interface IVisualEffectPoolObject : IPoolObject
    {
        public EffectInteractor EffectInteractor { get; }
    }
}