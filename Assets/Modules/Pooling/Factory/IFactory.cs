using System;
using Cysharp.Threading.Tasks;

namespace Modules.Pooling.Factory
{
    public interface IFactory<out TObject> : IDisposable
    {
        TObject Create();

        UniTask Load();
    }
}