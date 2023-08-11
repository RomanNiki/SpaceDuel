using Core.Explosions.Components;
using Core.Extensions;
using Core.Player.Components;
using Core.Sun.Components;
using Core.Weapon.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Factories.EntitiesFactories.Projectiles
{
    [CreateAssetMenu(fileName = "Mine", menuName = "SpaceDuel/Projectiles/Mine", order = 10)]
    public class MineEntityFactorySo : ProjectileEntityFactorySo
    {
        [SerializeField] private float _startEnergy = 40;
        
        protected override void OnCreateEntity(Entity entity, in World world)
        {
            world.AddComponentToEntity(entity, new MineTag());
            world.AddComponentToEntity(entity, new SunDischargeTag());
            world.AddComponentToEntity(entity, new ExplosiveTag());
            world.AddComponentToEntity(entity, new GravityResistTag());
            world.AddComponentToEntity(entity, new Energy(_startEnergy));
        }
    }
}