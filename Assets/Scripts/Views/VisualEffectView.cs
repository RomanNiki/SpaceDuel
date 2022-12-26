using Model.Components.Extensions.Pool;
using UnityEngine;
using UnityEngine.VFX;
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