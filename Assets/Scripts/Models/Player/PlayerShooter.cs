using Player.Weapon;
using UnityEngine.InputSystem;

namespace Models.Player
{
    public class PlayerShooter
    {
        private readonly DefaultGun _firstWeapon;
        private readonly DefaultGun _secondWeapon;

        public PlayerShooter(DefaultGun firstWeapon, DefaultGun secondWeapon)
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