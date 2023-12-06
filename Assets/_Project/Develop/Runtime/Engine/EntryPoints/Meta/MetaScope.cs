using _Project.Develop.Runtime.Engine.UI.Menu;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.EntryPoints.Meta
{
    public class MetaScope : LifetimeScope
    {
        [FormerlySerializedAs("_menu")] [SerializeField] private MenuView _menuView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMenu(builder);
            builder.RegisterEntryPoint<MetaFlow>();
        }

        private void RegisterMenu(IContainerBuilder builder) => builder.RegisterInstance(_menuView);
    }
}