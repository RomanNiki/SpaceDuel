using System;
using Core.Views.Components;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;

namespace Core.Common
{
    public interface IAssets : IDisposable
    {
        Entity Create(SpawnRequest spawnRequest, World world);
        UniTask Load();
    }
}