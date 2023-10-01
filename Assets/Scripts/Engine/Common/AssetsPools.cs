using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common;
using Core.Common.Enums;
using Core.Services;
using Core.Views.Components;
using Cysharp.Threading.Tasks;
using Engine.Factories;
using Engine.Pools;
using Engine.Providers;
using Modules.Pooling.Core;
using Modules.Pooling.Core.Factory;
using Modules.Pooling.Core.Pool;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Engine.Common
{
    public class AssetsPools : IAssets, ILoadingResource
    {
        private readonly Dictionary<ObjectId, IFactory<SpawnRequest, World, EntityProvider>> _pools = new();
        private readonly List<AssetPair> _assetReferences = new();

        public AssetsPools()
        {
        }

        public AssetsPools(IEnumerable<AssetPair> assetPairs)
        {
            foreach (var assetPair in assetPairs)
            {
                _assetReferences.Add(assetPair);
            }
        }

        private void AddPool(ObjectId objectId, IFactory<SpawnRequest, World, EntityProvider> pool)
        {
            var isExists = _pools.TryGetValue(objectId, out _);
#if DEBUG
            if (isExists)
            {
                throw new ArgumentException($"Pool for key: {objectId} is exists");
            }
#endif
            _pools.Add(objectId, pool);
        }

        public void Add(ObjectId objectId, AssetReference reference, int initializeSize = 10)
        {
            var assetPair = new AssetPair(objectId, reference, initializeSize);
            Add(assetPair);
        }

        public void Add(AssetPair pair)
        {
            if (_assetReferences.Contains(pair) == false)
            {
                _assetReferences.Add(pair);
            }
        }

        private async UniTask CreatePool(ObjectId objectId, AssetReference reference, int initializeSize)
        {
            var settings = PoolBase<PoolableEntityProvider>.Settings.Default;
            settings.InitialSize = initializeSize;
            var factory = new AddressableViewFactory<PoolableEntityProvider>(reference);
            var pool = new EntityProviderPool(settings, factory, objectId.ToString());
            await pool.Load();
            AddPool(objectId, pool);
        }

        public async UniTask<Entity> Create(SpawnRequest spawnRequest, World world)
        {
            var (success, entity) = await TryCreateEntity(spawnRequest, world);
            if (success == false)
            {
                await Load();
                (success, entity) = await TryCreateEntity(spawnRequest, world);
            }

            if (success)
            {
                return entity;
            }

            throw new KeyNotFoundException($"Key [{spawnRequest.Id}] was not found");
        }

        public UniTask Preload()
        {
            throw new NotImplementedException();
        }

        private async UniTask<(bool, Entity)> TryCreateEntity(SpawnRequest spawnRequest, World world)
        {
            if (_pools.TryGetValue(spawnRequest.Id, out var factory))
            {
                var entity = await CreateEntity(spawnRequest, world, factory);
                return (true, entity);
            }


            return (false, null);
        }

        private static async Task<Entity> CreateEntity(SpawnRequest spawnRequest, World world,
            IFactory<SpawnRequest, World, EntityProvider> factory)
        {
            var entityProvider = await factory.Create(spawnRequest, world);
            entityProvider.transform.position = spawnRequest.Position;
            entityProvider.transform.rotation = Quaternion.Euler(0, 0, spawnRequest.Rotation);
            return entityProvider.Entity;
        }

        public void Dispose()
        {
            foreach (var (_, factory) in _pools)
            {
                factory.Dispose();
            }

            _pools.Clear();
        }

        public void Cleanup()
        {
            foreach (var (_, factory) in _pools)
            {
                factory.Cleanup();
            }
        }

        public async UniTask Load()
        {
            foreach (var reference in _assetReferences)
            {
                if (_pools.TryGetValue(reference.Id, out _))
                {
                    continue;
                }
                
                await CreatePool(reference.Id, reference.AssetReference, reference.InitializeSize);
            }
        }
    }
}