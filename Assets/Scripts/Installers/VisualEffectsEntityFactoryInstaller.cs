using Extensions.MappingUnityToModel.Factories;
using Model.Extensions.EntityFactories;
using Model.VisualEffects;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class VisualEffectsEntityFactoryInstaller : MonoInstaller
    {
        [SerializeField] private VisualEffectEntityFactoryFromSo _explosionEntityFactory;
        [SerializeField] private VisualEffectEntityFactoryFromSo _hitEntityFactory;

        public override void InstallBindings()
        {
            Container.BindInstance<IEntityFactory>(_explosionEntityFactory).WhenInjectedInto<ExecuteExplosionSystem>();
            Container.BindInstance<IEntityFactory>(_hitEntityFactory).WhenInjectedInto<ExecuteHitSystem>();
        }
    }
}