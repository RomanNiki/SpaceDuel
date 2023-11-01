using System;
using Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;
using TriInspector;
#if UNITY_EDITOR
using Scellecs.Morpeh.Editor;
#endif

namespace Engine.Providers
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public class EntityProvider : EntityProviderBase, IDisposable
    {
        public World World { get; private set; }
        public Entity Entity { get; private set; }
        private IProvider[] _providers;
        public event Action EntityInit;
        public event Action EntityDispose;

        private void Awake()
        {
            _providers = GetComponents<IProvider>();
        }
        
        public override void Resolve(World world, Entity entity)
        {
            if (Entity.IsNullOrDisposed() == false)
            {
                world.RemoveEntity(Entity);
            }

            Entity = entity;
            World = world;

#if UNITY_EDITOR
            _entityViewer.getter = () => Entity;
#endif
            
            foreach (var provider in _providers)
            {
                if (provider is not EntityProviderBase)
                {
                    provider.Resolve(World, Entity);
                }
            }
            
            EntityInit?.Invoke();
        }

        public void Dispose()
        {
            EntityDispose?.Invoke();
            if (Entity.IsNullOrDisposed() == false)
                World.RemoveEntity(Entity);
            World = null;
        }

#if UNITY_EDITOR
        [PropertyOrder(100)] [ShowInInspector] [InlineProperty] [HideLabel] [Title("Debug Info", HorizontalLine = true)]
        private readonly EntityViewer _entityViewer = new();
#endif
    }
}