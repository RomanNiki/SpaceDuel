using System.Collections.Generic;
using Core.Common;
using Core.Common.Enums;
using Core.Views.Components;
using Cysharp.Threading.Tasks;
using Modules.Pooling.Core.Factory;
using Scellecs.Morpeh;

namespace Engine.Common
{
    public class AssetsPools : IAssets
    {
        private readonly Dictionary<ObjectId, IFactory<SpawnRequest, World, Entity>> _pools = new();

        public AssetsPools()
        {
        }

        public AssetsPools(params (ObjectId, IFactory<SpawnRequest, World, Entity>)[] pools)
        {
            foreach (var (id, factory) in pools)
            {
                _pools.Add(id, factory);
            }
        }

        public void Dispose()
        {
            foreach (var (_, factory) in _pools)
            {
                factory.Dispose();
            }

            _pools.Clear();
        }

        public void AddPool(ObjectId objectId, IFactory<SpawnRequest, World, Entity> pool) => _pools.Add(objectId, pool);

        public async UniTask Load()
        {
            foreach (var (_, factory) in _pools)
            {
                await factory.Load();
            }
        }

        public Entity Create(SpawnRequest spawnRequest, World world)
        {
            return _pools.TryGetValue(spawnRequest.Id, out var factory)
                ? factory.Create(spawnRequest, world)
                : throw new KeyNotFoundException();
        }
    }
}