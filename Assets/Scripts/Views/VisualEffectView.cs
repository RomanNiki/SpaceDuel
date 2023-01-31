using UnityEngine;
using UnityEngine.VFX;
using Views.Extensions.Pools;
using Zenject;

namespace Views
{
    public class VisualEffectView : GameObjectView, IVisualEffectPoolObject
    {
        [SerializeField] private VisualEffect _visualEffect;
        public VisualEffect VisualEffect => _visualEffect;

        public new class Factory : PlaceholderFactory<VisualEffectView>
        {
        }
    }
}