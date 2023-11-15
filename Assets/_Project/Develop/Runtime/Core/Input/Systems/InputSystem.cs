using _Project.Develop.Runtime.Core.Common.Enums;
using _Project.Develop.Runtime.Core.Extensions;
using _Project.Develop.Runtime.Core.Input.Components;
using Scellecs.Morpeh;

namespace _Project.Develop.Runtime.Core.Input.Systems
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
  
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public sealed class InputSystem : ISystem
    {
        private readonly IInput _input;

        public InputSystem(IInput input)
        {
            _input = input;
        }

        public World World { get; set; }

        public void OnUpdate(float deltaTime)
        {
        }

        public void OnAwake()
        {
            _input.StartAccelerate += OnStartAccelerate;
            _input.CancelAccelerate += OnCancelAccelerate;
            _input.StartRotate += OnStartRotate;
            _input.CancelRotate += OnCancelRotate;
            _input.StartPrimaryShot += OnPrimaryWeaponShotStart;
            _input.CancelPrimaryShot += OnPrimaryWeaponShotCancel;
            _input.StartSecondaryShot += OnSecondaryWeaponShotStart;
            _input.CancelSecondaryShot += OnSecondaryWeaponShotCancel;
            _input.Menu += OnMenu;
        }

        private void SendMessageInGame<T>(in T messageEvent)
            where T : struct, IComponent
        {
            World.SendMessage(messageEvent);
        }

        private void OnPrimaryWeaponShotStart(TeamEnum teamEnum) =>
            SendMessageInGame(new InputShootStartedEvent { PlayerTeam = teamEnum, Weapon = WeaponEnum.Primary });

        private void OnSecondaryWeaponShotStart(TeamEnum teamEnum) =>
            SendMessageInGame(new InputShootStartedEvent { PlayerTeam = teamEnum, Weapon = WeaponEnum.Secondary });

        private void OnPrimaryWeaponShotCancel(TeamEnum teamEnum) =>
            SendMessageInGame(new InputShootCanceledEvent() { PlayerTeam = teamEnum, Weapon = WeaponEnum.Primary });

        private void OnSecondaryWeaponShotCancel(TeamEnum teamEnum) =>
            SendMessageInGame(new InputShootCanceledEvent { PlayerTeam = teamEnum, Weapon = WeaponEnum.Secondary });

        private void OnStartRotate(TeamEnum teamEnum, float axis) =>
            SendMessageInGame(new InputRotateStartedEvent() { PlayerTeam = teamEnum, Axis = axis });

        private void OnCancelRotate(TeamEnum teamEnum) =>
            SendMessageInGame(new InputRotateCanceledEvent { PlayerTeam = teamEnum });


        private void OnStartAccelerate(TeamEnum teamEnum) =>
            SendMessageInGame(new InputAccelerateStartedEvent() { PlayerTeam = teamEnum });

        private void OnCancelAccelerate(TeamEnum teamEnum) =>
            SendMessageInGame(new InputAccelerateCanceledEvent { PlayerTeam = teamEnum });
        

        private void OnMenu()
        {
            SendMessageInGame(new InputPauseQuitEvent());
        }

        public void Dispose()
        {
            _input.StartAccelerate -= OnStartAccelerate;
            _input.CancelAccelerate -= OnCancelAccelerate;
            _input.StartRotate -= OnStartRotate;
            _input.CancelRotate -= OnCancelRotate;
            _input.StartPrimaryShot -= OnPrimaryWeaponShotStart;
            _input.CancelPrimaryShot -= OnPrimaryWeaponShotCancel;
            _input.StartSecondaryShot -= OnSecondaryWeaponShotStart;
            _input.CancelSecondaryShot -= OnSecondaryWeaponShotCancel;
            _input.Menu -= OnMenu;
        }
    }
}