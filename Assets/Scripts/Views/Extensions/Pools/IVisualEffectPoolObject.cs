using Model.Extensions.Interfaces.Pool;
using UnityEngine;
using UnityEngine.VFX;

namespace Views.Extensions.Pools
{
    public interface IVisualEffectPoolObject : IPoolObject
    {
        public EffectInteractor EffectInteractor { get; }
    }
}