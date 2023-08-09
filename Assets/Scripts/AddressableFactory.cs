using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class AddressableFactory<T> : IFactory<T>, IDisposable where T : Component
{
    private readonly AssetReference _reference;
    private readonly DiContainer _container;
    private T _prefab;

    public AddressableFactory(AssetReference reference, DiContainer container)
    {
        _reference = reference;
        _container = container;
        LoadAsset().Forget();
    }

    private async UniTaskVoid LoadAsset()
    {
        _prefab = await _reference.LoadAssetAsync<T>();
    }

    public T Create()
    {
        return _container.InstantiatePrefabForComponent<T>(_prefab);
    }

    public void Dispose()
    {
        _reference.ReleaseAsset();
    }
}