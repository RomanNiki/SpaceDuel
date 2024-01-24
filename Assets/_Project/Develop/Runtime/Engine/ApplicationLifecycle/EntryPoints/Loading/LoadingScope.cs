using VContainer;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Engine.ApplicationLifecycle.EntryPoints.Loading
{
    public class LoadingScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LoadingFlow>();
        }
    }
}