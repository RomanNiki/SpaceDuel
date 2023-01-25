using Model.Extensions.Interfaces.Pool;
using UnityEngine.VFX;

namespace Views.Extensions.Pools
{
    public interface IVisualEffectPoolObject : IPoolObject
    {
        public VisualEffect VisualEffect { get; }
    }
}