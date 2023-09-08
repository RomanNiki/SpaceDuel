using Core.Extensions;
using Core.Weapon.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Engine.Factories.EntitiesFactories.Projectiles
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