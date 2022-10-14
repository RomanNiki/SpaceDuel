using System;
using UnityEngine.InputSystem;

namespace Models.Player
{
    public class PlayerInputRouter : IDisposable
    {
        private readonly PlayerShooter _shooter;
        private readonly InputActionMap _input;

        public PlayerInputRouter(PlayerShooter shooter, InputActionMap input)
        {
            _input = input;
            _input.Enable();
            _shooter = shooter;
            Rotate = _input.FindAction(nameof(Rotate), throwIfNotFound: true);
            Acceleration = _input.FindAction(nameof(Acceleration), throwIfNotFound: true);
            FirstShoot = _input.FindAction(nameof(FirstShoot), throwIfNotFound: true);
            SecondShoot = _input.FindAction(nameof(SecondShoot), throwIfNotFound: true);
            SecondShoot.performed += _shooter.FirstWeaponShoot;
            FirstShoot.performed += _shooter.SecondaryWeaponShoot;
        }

        public InputAction SecondShoot { get; set; }

        public InputAction FirstShoot { get; set; }

        public InputAction Rotate { get; set; }

        public InputAction Acceleration { get; set; }

        public float Rotation => Rotate.ReadValue<float>();
        public bool Accelerate => Acceleration.phase == InputActionPhase.Performed;

        public void Dispose()
        {
            _input.Disable();
            FirstShoot.performed -= _shooter.FirstWeaponShoot;
            SecondShoot.performed -= _shooter.SecondaryWeaponShoot;
        }
    }
}