using System;
using Core.Views.Components;
using Cysharp.Threading.Tasks;
using Modules.Pooling.Core;
using Scellecs.Morpeh;

namespace Core.Services
{
    public interface IAssets : IDisposable, ICleanup
    {
        UniTask<Entity> Create(SpawnRequest spawnRequest, World world);
    }
}