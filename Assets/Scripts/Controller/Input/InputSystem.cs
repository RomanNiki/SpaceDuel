using Leopotam.Ecs;
using Model.Enums;
using Model.Extensions;
using Model.Unit.Input.Components.Events;
using Zenject;

namespace Controller.Input
{
    internal sealed class InputSystem : IEcsInitSystem, IEcsDestroySystem
    {
        [Inject] private PlayerInput _playerInput;
        private readonly EcsWorld _world = null;
        
        private void SendMessageInGame<T>(in T messageEvent)
            where T : struct
        {
            _world.SendMessage(messageEvent);
        }

        public void Init()
        {
            _playerInput.Enable();
            _playerInput.Common.Menu.started += _ =>
                SendMessageInGame(new InputPauseQuitEvent());
            _playerInput.Common.AnyKey.performed += _ => _world.SendMessage(new InputAnyKeyEvent());
            InitMoveInput();
            InitShootInput();
        }

        private void InitShootInput()
        {
            _playerInput.Player.FirstShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent
                    {PlayerTeamEnum = TeamEnum.Blue, Weapon = WeaponEnum.Primary});

            _playerInput.Player1.FirstShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent
                    {PlayerTeamEnum = TeamEnum.Red, Weapon = WeaponEnum.Primary});

            _playerInput.Player.FirstShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent
                    {PlayerTeamEnum = TeamEnum.Blue, Weapon = WeaponEnum.Primary});

            _playerInput.Player1.FirstShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent
                    {PlayerTeamEnum = TeamEnum.Red, Weapon = WeaponEnum.Primary});

            _playerInput.Player.SecondShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent
                    {PlayerTeamEnum = TeamEnum.Blue, Weapon = WeaponEnum.Secondary});

            _playerInput.Player1.SecondShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent
                    {PlayerTeamEnum = TeamEnum.Red, Weapon = WeaponEnum.Secondary});

            _playerInput.Player.SecondShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent
                    {PlayerTeamEnum = TeamEnum.Blue, Weapon = WeaponEnum.Secondary});

            _playerInput.Player1.SecondShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent
                    {PlayerTeamEnum = TeamEnum.Red, Weapon = WeaponEnum.Secondary});
        }

        private void InitMoveInput()
        {
            _playerInput.Player.Rotate.started += context =>
                SendMessageInGame(new InputRotateStartedEvent()
                    {PlayerTeam = TeamEnum.Blue, Axis = context.ReadValue<float>()});

            _playerInput.Player1.Rotate.started += context =>
                SendMessageInGame(new InputRotateStartedEvent
                    {PlayerTeam = TeamEnum.Red, Axis = context.ReadValue<float>()});

            _playerInput.Player.Rotate.canceled += _ =>
                SendMessageInGame(new InputRotateCanceledEvent() {PlayerNumber = TeamEnum.Blue});

            _playerInput.Player1.Rotate.canceled += _ =>
                SendMessageInGame(new InputRotateCanceledEvent {PlayerNumber = TeamEnum.Red});

            _playerInput.Player.Acceleration.started += _ =>
                SendMessageInGame(new InputAccelerateStartedEvent() {PlayerTeam = TeamEnum.Blue});

            _playerInput.Player1.Acceleration.started += _ =>
                SendMessageInGame(new InputAccelerateStartedEvent {PlayerTeam = TeamEnum.Red});

            _playerInput.Player.Acceleration.canceled += _ =>
                SendMessageInGame(new InputAccelerateCanceledEvent() {PlayerTeam = TeamEnum.Blue});

            _playerInput.Player1.Acceleration.canceled += _ =>
                SendMessageInGame(new InputAccelerateCanceledEvent {PlayerTeam = TeamEnum.Red});
        }

        public void Destroy()
        {
            _playerInput.Dispose();
            _playerInput.Disable();
        }
    }
}