using Core.Common.Enums;
using Core.Extensions;
using Core.Input.Components;
using Scellecs.Morpeh;
using UnityEngine.InputSystem;

namespace Engine.Input.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public sealed class InputSystem : IInitializer, PlayerInput.IPlayer1Actions, PlayerInput.IPlayerActions,
        PlayerInput.ICommonActions
    {
        private readonly PlayerInput _playerInput;

        public InputSystem(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _playerInput.Enable();
            _playerInput.Player1.SetCallbacks(this);
            _playerInput.Player.SetCallbacks(this);
            _playerInput.Common.SetCallbacks(this);
        }

        private void SendMessageInGame<T>(in T messageEvent)
            where T : struct, IComponent
        {
            World.SendMessage(messageEvent);
        }

        private void ShootStartedEvent(TeamEnum teamEnum, WeaponEnum weaponEnum) =>
            SendMessageInGame(new InputShootStartedEvent { PlayerTeamEnum = teamEnum, Weapon = weaponEnum });


        private void ShootCanceledEvent(TeamEnum teamEnum, WeaponEnum weaponEnum) =>
            SendMessageInGame(new InputShootCanceledEvent { PlayerTeam = teamEnum, Weapon = weaponEnum });


        private void OnRotateAction(InputAction.CallbackContext callbackContext, TeamEnum teamEnum)
        {
            if (callbackContext.started)
            {
                SendMessageInGame(new InputRotateStartedEvent()
                    { PlayerTeam = teamEnum, Axis = callbackContext.ReadValue<float>() });
            }

            if (callbackContext.canceled)
            {
                SendMessageInGame(new InputRotateCanceledEvent { PlayerTeam = teamEnum });
            }
        }

        private void OnAccelerateAction(InputAction.CallbackContext callbackContext, TeamEnum teamEnum)
        {
            if (callbackContext.started)
            {
                SendMessageInGame(new InputAccelerateStartedEvent()
                    { PlayerTeam = teamEnum });
            }

            if (callbackContext.canceled)
            {
                SendMessageInGame(new InputAccelerateCanceledEvent { PlayerTeam = teamEnum });
            }
        }

        private void OnShootAction(InputAction.CallbackContext callbackContext, TeamEnum teamEnum,
            WeaponEnum weaponEnum)
        {
            if (callbackContext.started)
            {
                ShootStartedEvent(teamEnum, weaponEnum);
            }

            if (callbackContext.canceled)
            {
                ShootCanceledEvent(teamEnum, weaponEnum);
            }
        }

        #region RedPlayer

        void PlayerInput.IPlayer1Actions.OnRotate(InputAction.CallbackContext context) =>
            OnRotateAction(context, TeamEnum.Red);


        void PlayerInput.IPlayer1Actions.OnAcceleration(InputAction.CallbackContext context) =>
            OnAccelerateAction(context, TeamEnum.Red);

        void PlayerInput.IPlayer1Actions.OnFirstShoot(InputAction.CallbackContext context) =>
            OnShootAction(context, TeamEnum.Red, WeaponEnum.Primary);


        void PlayerInput.IPlayer1Actions.OnSecondShoot(InputAction.CallbackContext context) =>
            OnShootAction(context, TeamEnum.Red, WeaponEnum.Secondary);

        #endregion

        #region BluePlayer

        void PlayerInput.IPlayerActions.OnRotate(InputAction.CallbackContext context) =>
            OnRotateAction(context, TeamEnum.Blue);


        void PlayerInput.IPlayerActions.OnAcceleration(InputAction.CallbackContext context) =>
            OnAccelerateAction(context, TeamEnum.Blue);


        void PlayerInput.IPlayerActions.OnFirstShoot(InputAction.CallbackContext context) =>
            OnShootAction(context, TeamEnum.Blue, WeaponEnum.Primary);


        void PlayerInput.IPlayerActions.OnSecondShoot(InputAction.CallbackContext context) =>
            OnShootAction(context, TeamEnum.Blue, WeaponEnum.Secondary);

        #endregion

        public void OnMenu(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SendMessageInGame(new InputPauseQuitEvent());
            }
        }

        public void Dispose()
        {
            _playerInput?.Disable();
            _playerInput?.Dispose();
        }
    }
}