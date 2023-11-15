using _Project.Develop.Runtime.Core.Weapon.Components;
using _Project.Develop.Runtime.Core.Weapon.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace _Project.Develop.Runtime.Core.Weapon
{
    public class WeaponFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            RegisterEvent<ShotMadeEvent>();
            AddSystem(new ShootDeniedTimeBetweenShotsSystem());
            AddSystem(new ExecuteShootSystem());
            AddSystem(new GunTimerBetweenShotsStartSystem());
            RegisterRequest<ShootingRequest>();
        }
    }
}