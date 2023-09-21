using Engine.Common.EntityConfigs;
using Engine.Providers.MonoProviders.Base;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Providers.MonoProviders.Config
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