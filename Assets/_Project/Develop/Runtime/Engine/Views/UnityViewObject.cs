using System;
using _Project.Develop.Runtime.Engine.Services.Movement.Strategies;
using JetBrains.Annotations;
using UnityEngine;
using EntityProvider = _Project.Develop.Runtime.Engine.Providers.EntityProvider;

namespace _Project.Develop.Runtime.Engine.Views
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [Serializable]
    public sealed class UnityViewObject : ViewObjectBase
    {
        private readonly IMoveStrategy _moveStrategy;
        private readonly EntityProvider _provider;
        [NotNull] private readonly Transform _transform;

        public UnityViewObject([NotNull] EntityProvider provider, IMoveStrategy moveStrategy)
        {
            _moveStrategy = moveStrategy;
            _transform = provider.transform;
            _provider = provider;
        }

        public override void MoveTo(Vector2 position) => _moveStrategy?.MoveTo(new Vector3(position.x, position.y, 0f));

        public override void RotateTo(float rotation) => _transform.rotation = Quaternion.Euler(0f, 0f, rotation);

        public override void Dispose()
        {
            if (_provider == null) return;
            _provider.Dispose();
        }
    }
}