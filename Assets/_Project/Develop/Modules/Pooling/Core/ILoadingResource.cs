using Cysharp.Threading.Tasks;

namespace _Project.Develop.Modules.Pooling.Core
{
    public interface ILoadingResource
    {
        UniTask Load();
    }
}