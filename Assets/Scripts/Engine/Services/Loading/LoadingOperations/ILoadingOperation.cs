using System;
using Cysharp.Threading.Tasks;

namespace Engine.Services.Loading.LoadingOperations
{
    public interface ILoadingOperation
    {
        string Description { get;}
        UniTask Load(Action<float> onProgress);
    }
}
