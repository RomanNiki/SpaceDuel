using Core.Movement.Components;
using Core.Views.Components;
using Cysharp.Threading.Tasks;
using Engine.Providers;
using Modules.Pooling.Core.Factory;
using Modules.Pooling.Engine.Pools;
using Scellecs.Morpeh;

namespace Engine.Pools
{
    public class EntityProviderPool : MonoPool<PoolableEntityProvider>, IFactory<SpawnRequest, World, EntityProvider>
    {
        private World _world;
        private Stash<Rotation> _rotationPool;
        private Stash<Position> _positionPool;
        private Stash<Position> PositionPool => _positionPool ??= _world.GetStash<Position>();
        private Stash<Rotation> RotationPool => _rotationPool ??= _world.GetStash<Rotation>();

        public EntityProviderPool(Settings settings, IFactory<PoolableEntityProvider> factory, string name) : base(
            settings, factory, name)
        {
        }

        public async UniTask<EntityProvider> Create(SpawnRequest data, World world)
        {
            _world ??= world;
            var provider = await Create();
            provider.Init(world, data.Entity);

            PositionPool.Set(data.Entity, new Position { Value = data.Position });
            RotationPool.Set(data.Entity, new Rotation { Value = data.Rotation });

            return provider;
        }
    }
}