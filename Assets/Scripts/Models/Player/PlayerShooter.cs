using Models.Pause;
using Models.Player.Weapon;
using UnityEngine.InputSystem;
using Zenject;

namespace Models.Player
{
    public sealed class PlayerShooter : IPauseHandler
    {
        private readonly DefaultGun _firstWeapon;
        private readonly DefaultGun _secondWeapon;
        private bool _isPause;

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
            if (gun.CanShoot() && _isPause == false)
                gun.Shoot();
        }

        public void SetPaused(bool isPaused)
        {
            _isPause = isPaused;
        }
    }
}