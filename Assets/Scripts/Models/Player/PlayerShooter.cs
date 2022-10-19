using Models.Player.Weapon;
using UnityEngine.InputSystem;
using Zenject;

namespace Models.Player
{
    public class PlayerShooter
    {
        private readonly DefaultGun _firstWeapon;
        private readonly DefaultGun _secondWeapon;

        public PlayerShooter([Inject (Id = WeaponEnum.Primary)]DefaultGun firstWeapon, [Inject (Id = WeaponEnum.Secondary)] DefaultGun secondWeapon)
        {
            _firstWeapon = firstWeapon;
            _secondWeapon = secondWeapon;
        }
        
        public void FirstWeaponShoot(InputAction.CallbackContext callbackContext)
        {
            TryShoot(_firstWeapon);
        }

        public void SecondaryWeaponShoot(InputAction.CallbackContext callbackContext)
        {
            TryShoot(_secondWeapon);
        }
        
        private void TryShoot(DefaultGun gun)
        {
            if (gun.CanShoot())
                gun.Shoot();
        }
    }
}