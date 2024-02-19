using System;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Engine.Providers
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public class EntityProvider : EntityProviderBase, IDisposable
    {
        private IProvider[] _providers;
        public World World { get; private set; }
        public Entity Entity { get; private set; }

        private void Awake()
        {
            _providers = GetComponents<IProvider>();
        }

        public void Dispose()
        {
            EntityDispose?.Invoke();
            if (Entity.IsNullOrDisposed() == false)
                World.RemoveEntity(Entity);
            World = null;
        }

        public event Action EntityInit;
        public event Action EntityDispose;

        public override void Resolve(World world, Entity entity)
        {
            if (Entity.IsNullOrDisposed() == false)
            {
                world.RemoveEntity(Entity);
            }

            Entity = entity;
            World = world;
            
            foreach (var provider in _providers)
            {
                if (provider is not EntityProviderBase)
                {
                    provider.Resolve(World, Entity);
                }
            }
            
            EntityInit?.Invoke();
        }
    }
}