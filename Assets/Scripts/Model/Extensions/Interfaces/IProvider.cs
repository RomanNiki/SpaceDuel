using Cysharp.Threading.Tasks;

namespace Model.Extensions.Interfaces
{
    public interface IProvider<T>
    {
        UniTask<T>  Load();
        void Unload();
    }
}