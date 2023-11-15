using System;
using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Input;
using UnityEngine.InputSystem;

namespace _Project.Develop.Runtime.Engine.Input
{
    public class KeyboardInput : IInput, PlayerInput.IPlayerActions, PlayerInput.IPlayer1Actions,
        PlayerInput.ICommonActions, IDisposable
    {
        private readonly PlayerInput _playerInput;

        public KeyboardInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.Enable();
            _playerInput.Player.SetCallbacks(this);
            _playerInput.Player1.SetCallbacks(this);
            _playerInput.Common.SetCallbacks(this);
        }

        public void OnMenu(InputAction.CallbackContext context)
        {
            Menu?.Invoke();
        }

        public void Dispose()
        {
            _playerInput.Player.RemoveCallbacks(this);
            _playerInput.Player1.RemoveCallbacks(this);
            _playerInput.Common.RemoveCallbacks(this);
            _playerInput.Disable();
            _playerInput?.Dispose();
        }

        public event Action<TeamEnum> StartPrimaryShot;
        public event Action<TeamEnum> CancelPrimaryShot;
        public event Action<TeamEnum> StartSecondaryShot;
        public event Action<TeamEnum> CancelSecondaryShot;
        public event Action<TeamEnum> StartAccelerate;
        public event Action<TeamEnum> CancelAccelerate;
        public event Action<TeamEnum, float> StartRotate;
        public event Action<TeamEnum> CancelRotate;
        public event Action Menu;

        void PlayerInput.IPlayer1Actions.OnRotate(InputAction.CallbackContext context)
        {
            OnRotateAction(context, TeamEnum.Red);
        }

        void PlayerInput.IPlayer1Actions.OnAcceleration(InputAction.CallbackContext context)
        {
            OnAccelerateAction(context, TeamEnum.Red);
        }

        void PlayerInput.IPlayer1Actions.OnFirstShoot(InputAction.CallbackContext context)
        {
            OnShootAction(context, TeamEnum.Red, WeaponEnum.Primary);
        }

        void PlayerInput.IPlayer1Actions.OnSecondShoot(InputAction.CallbackContext context)
        {
            OnShootAction(context, TeamEnum.Red, WeaponEnum.Secondary);
        }

        void PlayerInput.IPlayerActions.OnRotate(InputAction.CallbackContext context)
        {
            OnRotateAction(context, TeamEnum.Blue);
        }

        void PlayerInput.IPlayerActions.OnAcceleration(InputAction.CallbackContext context)
        {
            OnAccelerateAction(context, TeamEnum.Blue);
        }

        void PlayerInput.IPlayerActions.OnFirstShoot(InputAction.CallbackContext context)
        {
            OnShootAction(context, TeamEnum.Blue, WeaponEnum.Primary);
        }

        void PlayerInput.IPlayerActions.OnSecondShoot(InputAction.CallbackContext context)
        {
            OnShootAction(context, TeamEnum.Blue, WeaponEnum.Secondary);
        }

        private void OnRotateAction(InputAction.CallbackContext context, TeamEnum teamEnum)
        {
            if (context.started)
            {
                StartRotate?.Invoke(teamEnum, context.ReadValue<float>());
            }
            else if (context.canceled)
            {
                CancelRotate?.Invoke(teamEnum);
            }
        }

        private void OnAccelerateAction(InputAction.CallbackContext context, TeamEnum teamEnum)
        {
            if (context.started)
            {
                StartAccelerate?.Invoke(teamEnum);
            }
            else if (context.canceled)
            {
                CancelAccelerate?.Invoke(teamEnum);
            }
        }

        private void OnShootAction(InputAction.CallbackContext callbackContext, TeamEnum teamEnum,
            WeaponEnum weaponEnum)
        {
            if (callbackContext.started)
            {
                switch (weaponEnum)
                {
                    case WeaponEnum.Primary:
                        StartPrimaryShot?.Invoke(teamEnum);
                        break;
                    case WeaponEnum.Secondary:
                        StartSecondaryShot?.Invoke(teamEnum);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(weaponEnum), weaponEnum, null);
                }
            }
            else if (callbackContext.canceled)
            {
                switch (weaponEnum)
                {
                    case WeaponEnum.Primary:
                        CancelPrimaryShot?.Invoke(teamEnum);
                        break;
                    case WeaponEnum.Secondary:
                        CancelSecondaryShot?.Invoke(teamEnum);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(weaponEnum), weaponEnum, null);
                }
            }
        }
    }
}