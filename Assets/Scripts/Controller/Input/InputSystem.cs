﻿using Components.Events.InputEvents;
using Leopotam.Ecs;
using Model.Components.Events.InputEvents;
using Model.Components.Extensions;
using Model.Enums;
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
            InitMoveInput();
            InitShootInput();
        }

        private void InitShootInput()
        {
            _playerInput.Player.FirstShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent { PlayerTeamEnum = TeamEnum.Blue, Weapon = WeaponEnum.Primary});

            _playerInput.Player1.FirstShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent {PlayerTeamEnum = TeamEnum.Red, Weapon = WeaponEnum.Primary});

            _playerInput.Player.FirstShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent {PlayerTeamEnum = TeamEnum.Blue, Weapon = WeaponEnum.Primary});

            _playerInput.Player1.FirstShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent {PlayerTeamEnum = TeamEnum.Red, Weapon = WeaponEnum.Primary});

            _playerInput.Player.SecondShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent {PlayerTeamEnum = TeamEnum.Blue, Weapon = WeaponEnum.Secondary});

            _playerInput.Player1.SecondShoot.started += _ =>
                SendMessageInGame(new InputShootStartedEvent {PlayerTeamEnum = TeamEnum.Red, Weapon = WeaponEnum.Secondary});

            _playerInput.Player.SecondShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent {PlayerTeamEnum = TeamEnum.Blue, Weapon = WeaponEnum.Secondary});

            _playerInput.Player1.SecondShoot.canceled += _ =>
                SendMessageInGame(new InputShootCanceledEvent {PlayerTeamEnum = TeamEnum.Red, Weapon = WeaponEnum.Secondary});
        }

        private void InitMoveInput()
        {
            _playerInput.Player.Rotate.started += context =>
                SendMessageInGame(new InputRotateStartedEvent() {PlayerNumber = TeamEnum.Blue, Axis = context.ReadValue<float>()});

            _playerInput.Player1.Rotate.started += context =>
                SendMessageInGame(new InputRotateStartedEvent {PlayerNumber = TeamEnum.Red, Axis = context.ReadValue<float>()});

            _playerInput.Player.Rotate.canceled += _ =>
                SendMessageInGame(new InputRotateCanceledEvent() {PlayerNumber = TeamEnum.Blue});

            _playerInput.Player1.Rotate.canceled += _ =>
                SendMessageInGame(new InputRotateCanceledEvent {PlayerNumber = TeamEnum.Red});

            _playerInput.Player.Acceleration.started += _ =>
                SendMessageInGame(new InputAccelerateEvent() {PlayerNumber = TeamEnum.Blue});

            _playerInput.Player1.Acceleration.started += _ =>
                SendMessageInGame(new InputAccelerateEvent {PlayerNumber = TeamEnum.Red});

            _playerInput.Player.Acceleration.canceled += _ =>
                SendMessageInGame(new InputAccelerateCanceledEvent() {PlayerNumber = TeamEnum.Blue});

            _playerInput.Player1.Acceleration.canceled += _ =>
                SendMessageInGame(new InputAccelerateCanceledEvent {PlayerNumber = TeamEnum.Red});
        }

        public void Destroy()
        {
            _playerInput.Disable();
        }
    }
}