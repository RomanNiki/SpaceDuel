using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using _Project.Develop.Runtime.Engine.Views.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.View
{
    public class MaterialTimerMonoProvider : MonoProviderBase
    {
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _endColor;
        [SerializeField] private float _intencity;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private int _materialIndex;
        

        public override void Resolve(World world, Entity entity)
        {
            var material = _renderer.materials[_materialIndex];
            world.GetStash<MaterialIndicator>().Set(entity,
                new MaterialIndicator()
                {
                    Material = material, Intencity = _intencity, EndColor = _endColor,
                    StartColor = _startColor
                });

            _renderer.materials[_materialIndex] = material;
        }
    }
}