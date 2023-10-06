using System;
using System.Threading.Tasks;
using Core.Views.Components;
using Modules.Pooling.Core;
using Scellecs.Morpeh;

namespace Core.Services
{
    public interface IAssets : IDisposable, ICleanup
    {
        Task<Entity> Create(SpawnRequest spawnRequest, World world);
    }
}