using System;
using Cysharp.Threading.Tasks;

namespace Modules.Pooling.Core.Factory
{
    public interface IFactory<TObject> : IDisposable
    {
        UniTask<TObject> Create();
    }

    public interface IFactory<in TArg1, in TArg2, TEntity> : IDisposable, ICleanup
    {
        UniTask<TEntity> Create(TArg1 arg1, TArg2 arg2);
    }
}