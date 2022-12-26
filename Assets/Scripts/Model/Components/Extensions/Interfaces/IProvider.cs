using Cysharp.Threading.Tasks;

namespace Model.Components.Extensions.Interfaces
{
    public interface IProvider<T>
    {
        UniTask<T>  Load();
        void Unload();
    }
}