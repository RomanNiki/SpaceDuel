using Core.Movement.Components;
using Core.Views.Components;
using Engine.Providers;
using Modules.Pooling.Core.Factory;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Views.Systems.Create
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif

    public sealed class ViewCreateSystem<TFlag> : ViewCreateSystemBase<ViewCreateRequest, TFlag>
        where TFlag : struct, IComponent
    {
        private readonly IFactory<EntityProvider> _factory;
        private Stash<Position> _positionPool;
        private Stash<Rotation> _rotationPool;

        private Stash<Position> PositionPool => _positionPool ??= World.GetStash<Position>();
        private Stash<Rotation> RotationPool => _rotationPool ??= World.GetStash<Rotation>();

        public ViewCreateSystem(IFactory<EntityProvider> factory) => _factory = factory;

        protected override Entity GetEntity(ViewCreateRequest createRequest) => createRequest.Entity;

        protected override EntityProvider CreateView(ViewCreateRequest viewCreateRequest) =>
            _factory.Create();

        protected override void SetData(Transform transform, in ViewCreateRequest data)
        {
            PositionPool.Set(data.Entity, new Position { Value = data.Position });
            RotationPool.Set(data.Entity, new Rotation { Value = data.Rotation });
            var eulerAngles = transform.eulerAngles;
            eulerAngles.z = data.Rotation;
            transform.eulerAngles = eulerAngles;
        }
    }
}