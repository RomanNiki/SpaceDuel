using System;
using Cysharp.Threading.Tasks;

namespace Extensions.Loading.LoadingOperations
{
    public interface ILoadingOperation
    {
        string Description { get;}
        UniTask Load(Action<float> onProgress);
    }
}
