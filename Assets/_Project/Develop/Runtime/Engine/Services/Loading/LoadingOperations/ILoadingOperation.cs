using System;
using Cysharp.Threading.Tasks;

namespace _Project.Develop.Runtime.Engine.Services.Loading.LoadingOperations
{
    public interface ILoadingOperation
    {
        string Description { get;}
        UniTask Load(Action<float> onProgress);
    }
}
