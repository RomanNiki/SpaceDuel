using Core.Enums;
using Core.Extensions;
using Core.Input.Components;
using Scellecs.Morpeh;

namespace Input
{
    public sealed class InputSystem : IInitializer
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
            _playerInput.Common.Menu.performed += _ => SendMessageInGame(new InputPauseQuitEvent());
            InitMoveInput();
            InitShootInput();
        }
        
        private void SendMessageInGame<T>(in T messageEvent)
            where T : struct, IComponent
        {
            World.SendMessage(messageEvent);
        }

        private void InitShootInput()
        {
            _playerInput.Player.FirstShoot.started += _ => ShootStartedEvent(TeamEnum.Blue, WeaponEnum.Primary);
            _playerInput.Player1.FirstShoot.started += _ => ShootStartedEvent(TeamEnum.Red, WeaponEnum.Primary);
            _playerInput.Player.FirstShoot.canceled += _ => ShootCanceledEvent(TeamEnum.Blue, WeaponEnum.Primary);
            _playerInput.Player1.FirstShoot.canceled += _ => ShootCanceledEvent(TeamEnum.Red, WeaponEnum.Primary);
            _playerInput.Player.SecondShoot.started += _ => ShootStartedEvent(TeamEnum.Blue, WeaponEnum.Secondary);
            _playerInput.Player1.SecondShoot.started += _ => ShootStartedEvent(TeamEnum.Red, WeaponEnum.Secondary);
            _playerInput.Player.SecondShoot.canceled += _ => ShootCanceledEvent(TeamEnum.Blue, WeaponEnum.Secondary);
            _playerInput.Player1.SecondShoot.canceled += _ => ShootCanceledEvent(TeamEnum.Red, WeaponEnum.Secondary);
        }

        private void ShootStartedEvent(TeamEnum teamEnum, WeaponEnum weaponEnum)
        {
            SendMessageInGame(new InputShootStartedEvent
                { PlayerTeamEnum = teamEnum, Weapon = weaponEnum });
        }

        private void ShootCanceledEvent(TeamEnum teamEnum, WeaponEnum weaponEnum)
        {
            SendMessageInGame(new InputShootCanceledEvent
                { PlayerTeamEnum = teamEnum, Weapon = weaponEnum });
        }

        private void InitMoveInput()
        {
            _playerInput.Player.Rotate.started += context =>
                SendMessageInGame(new InputRotateStartedEvent()
                    { PlayerTeam = TeamEnum.Blue, Axis = context.ReadValue<float>() });

            _playerInput.Player1.Rotate.started += context =>
                SendMessageInGame(new InputRotateStartedEvent
                    { PlayerTeam = TeamEnum.Red, Axis = context.ReadValue<float>() });

            _playerInput.Player.Rotate.canceled += _ =>
                SendMessageInGame(new InputRotateCanceledEvent() { PlayerNumber = TeamEnum.Blue });

            _playerInput.Player1.Rotate.canceled += _ =>
                SendMessageInGame(new InputRotateCanceledEvent { PlayerNumber = TeamEnum.Red });

            _playerInput.Player.Acceleration.started += _ =>
                SendMessageInGame(new InputAccelerateStartedEvent() { PlayerTeam = TeamEnum.Blue });

            _playerInput.Player1.Acceleration.started += _ =>
                SendMessageInGame(new InputAccelerateStartedEvent { PlayerTeam = TeamEnum.Red });

            _playerInput.Player.Acceleration.canceled += _ =>
                SendMessageInGame(new InputAccelerateCanceledEvent() { PlayerTeam = TeamEnum.Blue });

            _playerInput.Player1.Acceleration.canceled += _ =>
                SendMessageInGame(new InputAccelerateCanceledEvent { PlayerTeam = TeamEnum.Red });
        }

        public void Dispose()
        {
            _playerInput.Disable();
            _playerInput?.Dispose();
        }
    }
}