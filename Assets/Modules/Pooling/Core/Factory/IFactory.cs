using System;

namespace Modules.Pooling.Core.Factory
{
    public interface IFactory<TObject> : IDisposable
    {
        TObject Create();
    }

    public interface IFactory<in TArg1, in TArg2, TEntity> : IDisposable
    {
        TEntity Create(TArg1 arg1, TArg2 arg2);
    }
}