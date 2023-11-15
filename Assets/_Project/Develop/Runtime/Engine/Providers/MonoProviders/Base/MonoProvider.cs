using Scellecs.Morpeh;
using TriInspector;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base
{
    public class MonoProvider<T> : MonoProviderBase
        where T : struct, IComponent
    {
        [HideInInspector] [SerializeField] private T _serializedComponent;
        private Entity _cachedEntity;
        private Stash<T> _stash;

        public override void Resolve(World world, Entity entity)
        {
            if (entity.IsNullOrDisposed() == false)
            {
                _cachedEntity = entity;
                _stash = world.GetStash<T>();
                _stash.Set(entity, _serializedComponent);
                OnResolve(world, entity);
            }
        }

        protected virtual void OnResolve(World world, Entity entity)
        {
        }

#if UNITY_EDITOR
        private string GetTypeName() => typeof(T).Name;

        [Title("$GetTypeName")]
        [PropertySpace]
        [ShowInInspector]
        [PropertyOrder(1)]
        [HideLabel]
        [InlineProperty]
#endif

        protected T Data
        {
            get
            {
                if (_cachedEntity.IsNullOrDisposed()) return _serializedComponent;
                var data = _stash.Get(_cachedEntity, out var exist);
                return exist ? data : _serializedComponent;
            }
            set
            {
                if (_cachedEntity.IsNullOrDisposed() == false)
                {
                    _stash.Set(_cachedEntity, value);
                }
                else
                {
                    _serializedComponent = value;
                }
            }
        }
    }
}