using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Engine.Providers;
using Modules.Pooling.Core.Factory;

namespace Engine.Extensions
{
    public class ObjectPools : IDisposable
    {
        public IFactory<EntityProvider> BulletFactory { get; }
        public IFactory<EntityProvider> MineFactory { get; }
     //   private readonly IEnumerable<UniTask> _tasks;

        public ObjectPools(IFactory<EntityProvider> bulletFactory, IFactory<EntityProvider> mineFactory)
        {
            BulletFactory = bulletFactory;
            MineFactory = mineFactory;
            /*_tasks = new List<UniTask>
            {
                BulletFactory.Load(),
                MineFactory.Load()
            };*/
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