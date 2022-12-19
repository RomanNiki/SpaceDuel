using UnityEngine.VFX;

namespace Model.Components.Extensions.Pool
{
    public interface IVisualEffectPoolObject : IPoolObject
    {
        public VisualEffect VisualEffect { get; }
    }
}