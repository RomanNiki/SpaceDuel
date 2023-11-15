using _Project.Develop.Runtime.Engine.Common.EntityConfigs;
using _Project.Develop.Runtime.Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Providers.MonoProviders.Config
{
    public class ConfigMonoProvider : MonoProviderBase
    {
        [SerializeField] private EntityConfigSo _entityConfig;

        public override void Resolve(World world, Entity entity)
        {
            _entityConfig.Resolve(world, entity);
        }
    }
}