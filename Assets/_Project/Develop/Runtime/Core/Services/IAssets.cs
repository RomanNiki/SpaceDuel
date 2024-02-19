using System;
using _Project.Develop.Modules.Pooling.Core;
using _Project.Develop.Runtime.Core.Views.Components;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Services
{
    public interface IAssets : IDisposable, ICleanup, ILoadingResource
    {
        UniTask<Entity> Create(SpawnRequest spawnRequest, World world);
    }
}