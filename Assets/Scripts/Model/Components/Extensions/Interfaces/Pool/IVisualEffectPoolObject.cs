using UnityEngine.VFX;

namespace Model.Components.Extensions.Interfaces.Pool
{
    public interface IVisualEffectPoolObject : IPoolObject
    {
        public VisualEffect VisualEffect { get; }
    }
}