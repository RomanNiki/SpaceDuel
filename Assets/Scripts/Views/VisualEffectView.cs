using UnityEngine;
using Views.Extensions;
using Views.Extensions.Pools;
using Zenject;

namespace Views
{
    public class VisualEffectView : GameObjectView, IVisualEffectPoolObject
    {
        [SerializeField] private EffectInteractor _effectInteractor;
        public EffectInteractor EffectInteractor => _effectInteractor;
    }
    
    public class VisualEffectViewFactory : PlaceholderFactory<VisualEffectView>
    {
    }
}