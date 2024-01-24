using System;
using System.Collections.Generic;
using _Project.Develop.Modules.Pooling.Core;
using _Project.Develop.Modules.Pooling.Core.Factory;
using _Project.Develop.Modules.Pooling.Core.Pool;
using _Project.Develop.Modules.Pooling.Engine.Pools;
using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Movement.Aspects;
using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Views.Components;
using _Project.Develop.Runtime.Engine.Common;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.View;
using _Project.Develop.Runtime.Engine.Services.Factories;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace _Project.Develop.Runtime.Engine.Services.AssetManagement
{
    public class AssetsPools : IAssets
    {
        private readonly IObjectResolver _objectResolver;
        private readonly List<AssetPair> _assetReferences = new();
        private readonly Dictionary<ObjectId, IFactory<PoolMonoProvider>> _pools = new();

        public AssetsPools(IEnumerable<AssetPair> assetPairs, IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
            foreach (var assetPair in assetPairs)
            {
                _assetReferences.Add(assetPair);
            }
        }

        public async UniTask<Entity> Create(SpawnRequest spawnRequest, World world)
        {
            var (success, entity) = TryCreateEntity(spawnRequest, world);
            if (success == false)
            {
                await Load();
                (success, entity) = TryCreateEntity(spawnRequest, world);
            }

            if (success)
            {
                return entity;
            }

            throw new KeyNotFoundException($"Key [{spawnRequest.Id}] was not found");
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
        
        public void Cleanup()
        {
            foreach (var (_, pool) in _pools)
            {
                if (pool is ICleanup cleanup)
                {
                    cleanup.Cleanup();
                }
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

        private async UniTask CreatePool(ObjectId objectId, AssetReference reference, int initializeSize)
        {
            var settings = new PoolBase<PoolMonoProvider>.Settings(initializeSize);
            var factory = new AddressableViewFactory<PoolMonoProvider>(reference, _objectResolver);
            var pool = new MonoPool<PoolMonoProvider>(factory, objectId.ToString(), settings);
            if (pool is ILoadingResource loadingResource)
            {
                await loadingResource.Load();
            }

            AddPool(objectId, pool);
        }
        
        private void AddPool(ObjectId objectId, IFactory<PoolMonoProvider> pool)
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

        private (bool, Entity) TryCreateEntity(SpawnRequest spawnRequest, World world)
        {
            if (_pools.TryGetValue(spawnRequest.Id, out var factory))
            {
                var entity = CreateEntity(spawnRequest, world, factory);
                return (true, entity);
            }


            return (false, null);
        }

        private static Entity CreateEntity(SpawnRequest spawnRequest, World world,
            IFactory<PoolMonoProvider> factory)
        {
            var entityProvider = factory.Create();
            SetEntityTransform(spawnRequest, world, entityProvider);
            return spawnRequest.Entity;
        }

        private static void SetEntityTransform(SpawnRequest spawnRequest, World world, MonoProviderBase entityProvider)
        {
            var transformFactory = world.GetAspectFactory<TransformAspect>();
            var entity = spawnRequest.Entity;
            var aspectTransform = transformFactory.Get(entity);
            entityProvider.transform.position = spawnRequest.Position;
            entityProvider.transform.rotation = Quaternion.Euler(0, 0, spawnRequest.Rotation);
            entityProvider.Resolve(world, entity);
            aspectTransform.Position.Value = spawnRequest.Position;
            aspectTransform.Rotation.Value = spawnRequest.Rotation;
        }
    }
}