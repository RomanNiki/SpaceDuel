using Extensions.MappingUnityToModel.Factories;
using Model.Extensions;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class VisualEffectsEntityFactoryInstaller : MonoInstaller
    {
        [SerializeField] private EntityFactoryFromSo _explosionEntityFactory;
        [SerializeField] private EntityFactoryFromSo _hitEntityFactory;

        public override void InstallBindings()
        {
            var factory = new VisualEffectsEntityFactories(_explosionEntityFactory ,_hitEntityFactory);
            Container.Bind<VisualEffectsEntityFactories>().FromInstance(factory).AsSingle();
        }
    }
}