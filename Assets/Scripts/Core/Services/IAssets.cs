using System;
using System.Threading.Tasks;
using Core.Views.Components;
using Cysharp.Threading.Tasks;
using Modules.Pooling.Core;
using Scellecs.Morpeh;

namespace Core.Services
{
    public interface IAssets : IDisposable, ICleanup, ILoadingResource
    {
        UniTask<Entity> Create(SpawnRequest spawnRequest, World world);
    }
}