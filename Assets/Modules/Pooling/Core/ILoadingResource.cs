using Cysharp.Threading.Tasks;

namespace Modules.Pooling.Core
{
    public interface ILoadingResource
    {
        UniTask Load();
    }
}