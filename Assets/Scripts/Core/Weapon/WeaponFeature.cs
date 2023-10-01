using Core.Weapon.Components;
using Core.Weapon.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Core.Weapon
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