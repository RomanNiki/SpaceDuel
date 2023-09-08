using System;
using Cysharp.Threading.Tasks;
using Engine.Providers;
using Modules.Pooling.Factory;

namespace Engine.Extensions
{
    public class ObjectPools : IDisposable
    {
        public IFactory<EntityProvider> BulletFactory { get; }
        public IFactory<EntityProvider> MineFactory { get; }

        public ObjectPools(IFactory<EntityProvider> bulletFactory, IFactory<EntityProvider> mineFactory)
        {
            BulletFactory = bulletFactory;
            MineFactory = mineFactory;
        }

        public async UniTask Load()
        {
            await BulletFactory.Load();
            await MineFactory.Load();
        }

        public void Dispose()
        {
            BulletFactory.Dispose();
            MineFactory.Dispose();
        }
    }
}