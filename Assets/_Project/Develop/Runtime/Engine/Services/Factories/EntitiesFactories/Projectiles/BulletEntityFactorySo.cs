using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Weapon.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace _Project.Develop.Runtime.Engine.Services.Factories.EntitiesFactories.Projectiles
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "SpaceDuel/Projectiles/Bullet", order = 10)]
    public class BulletEntityFactorySo : ProjectileEntityFactorySo
    {
        protected override void OnCreateProjectileEntity(Entity entity, in World world)
        {
            world.AddComponentToEntity(entity, new BulletTag());
        }
    }
}