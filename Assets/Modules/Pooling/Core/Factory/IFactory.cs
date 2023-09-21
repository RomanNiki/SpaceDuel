using System;
using Cysharp.Threading.Tasks;

namespace Modules.Pooling.Core.Factory
{
    public interface IFactory<out TObject> : IDisposable
    {
        TObject Create();
        UniTask Load();
    }

    public interface IFactory<in TArg1, in TArg2, out TEntity> : IDisposable
    {
        TEntity Create(TArg1 arg1, TArg2 arg2);
        UniTask Load();
    }
}