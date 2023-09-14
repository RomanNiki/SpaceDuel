using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Modules.Pooling.Core.Factory
{
    public interface IFactory<out TObject> : IDisposable
    {
        TObject Create(Vector3 position = new(), float rotation = 0f);
        UniTask Load();
    }
}