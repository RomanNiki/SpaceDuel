using Components.Player.Move;
using Components.Player.MoveComponents;
using Events.InputEvents;
using Extensions;
using Leopotam.Ecs;
using Models;
using Models.Player;
using Tags;
using UnityEngine.InputSystem;
using Zenject;

namespace Systems
{
    internal sealed class InputSystem : IEcsInitSystem, IEcsDestroySystem
    {
        [Inject] private PlayerInput _playerInput;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerTag, InputRouter, InputData> _playerFilter = null;

        private void SendMessageInGame<T>(in T messageEvent)
            where T : struct
        {
            _world.SendMessage(messageEvent);
        }

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var inputComponent = ref _playerFilter.Get2(i);
                ref var inputData = ref _playerFilter.Get3(i);

                inputData.Accelerate = inputComponent.InputRoute.Accelerate;
                inputData.Rotation = inputComponent.InputRoute.Rotation;
                inputData.FirstShoot = inputComponent.InputRoute.FirstShoot.phase == InputActionPhase.Performed;
                inputData.SecondShoot = inputComponent.InputRoute.SecondShoot.phase == InputActionPhase.Performed;
            }
        }

        public void Init()
        {
            InitMoveInput();
            InitShootInput();
        }

        private void InitShootInput()
        {
            _playerInput.Player.FirstShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent {PlayerTeam = Team.Red, Weapon = WeaponEnum.Primary});

            _playerInput.Player1.FirstShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent {PlayerTeam = Team.Blue, Weapon = WeaponEnum.Primary});

            _playerInput.Player.FirstShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent {PlayerTeam = Team.Red, Weapon = WeaponEnum.Primary});

            _playerInput.Player1.FirstShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent {PlayerTeam = Team.Blue, Weapon = WeaponEnum.Primary});

            _playerInput.Player.SecondShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent {PlayerTeam = Team.Red, Weapon = WeaponEnum.Secondary});

            _playerInput.Player1.SecondShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent {PlayerTeam = Team.Blue, Weapon = WeaponEnum.Secondary});

            _playerInput.Player.SecondShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent {PlayerTeam = Team.Red, Weapon = WeaponEnum.Secondary});

            _playerInput.Player1.SecondShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent {PlayerTeam = Team.Blue, Weapon = WeaponEnum.Secondary});
        }

        private void InitMoveInput()
        {
            _playerInput.Player.Rotate.started += context =>
                SendMessageInGame(new InputRotateStartedEvent() {PlayerNumber = Team.Red, Axis = context.ReadValue<float>()});

            _playerInput.Player1.Rotate.started += context =>
                SendMessageInGame(new InputRotateStartedEvent {PlayerNumber = Team.Blue, Axis = context.ReadValue<float>()});

            _playerInput.Player.Rotate.canceled += _ =>
                SendMessageInGame(new InputRotateCanceledEvent() {PlayerNumber = Team.Red});

            _playerInput.Player1.Rotate.canceled += _ =>
                SendMessageInGame(new InputRotateCanceledEvent {PlayerNumber = Team.Blue});

            _playerInput.Player.Acceleration.started += _ =>
                SendMessageInGame(new InputAccelerateEvent() {PlayerNumber = Team.Red});

            _playerInput.Player1.Acceleration.started += _ =>
                SendMessageInGame(new InputAccelerateEvent {PlayerNumber = Team.Blue});

            _playerInput.Player.Acceleration.canceled += _ =>
                SendMessageInGame(new InputAccelerateCanceledEvent() {PlayerNumber = Team.Red});

            _playerInput.Player1.Acceleration.canceled += _ =>
                SendMessageInGame(new InputAccelerateCanceledEvent {PlayerNumber = Team.Blue});
        }

        public void Destroy()
        {
            _playerInput.Disable();
        }
    }
}