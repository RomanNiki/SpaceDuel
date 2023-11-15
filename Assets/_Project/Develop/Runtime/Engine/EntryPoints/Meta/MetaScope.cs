using _Project.Develop.Runtime.Engine.UI.Menu;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.EntryPoints.Meta
{
    public class MetaScope : LifetimeScope
    {
        [SerializeField] private Menu _menu;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMenu(builder);
            builder.RegisterEntryPoint<MetaFlow>();
        }

        private void RegisterMenu(IContainerBuilder builder)
        {
            builder.RegisterInstance(_menu);
        }
    }
}