using Core.Extensions;
using Core.Extensions.Clear.Systems;
using Core.Weapon.Components;
using Core.Weapon.Systems;
using Cysharp.Threading.Tasks;

namespace Core.Weapon
{
    public class WeaponFeature : BaseMorpehFeature
    {
        protected async override UniTask InitializeSystems()
        {
            AddSystem(new DellHereUpdateSystem<ShotMadeEvent>());
            AddSystem(new ShootDeniedTimeBetweenShotsSystem());
            AddSystem(new ExecuteShootSystem());
            AddSystem(new GunTimerBetweenShotsStartSystem());
            AddSystem(new DellHereUpdateSystem<ShootingRequest>());
        }
    }
}