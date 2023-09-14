using Cysharp.Threading.Tasks;

namespace Modules.Pooling.Core.EntityFactory
{
    public interface IEntityFactory<TObject>
    {
        public UniTask<TObject> CreateAsync();
    }
}