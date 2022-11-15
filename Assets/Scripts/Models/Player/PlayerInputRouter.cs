using System;
using UnityEngine.InputSystem;

namespace Models.Player
{
    public sealed class PlayerInputRouter : IDisposable
    {
        private readonly InputActionMap _input;

        public PlayerInputRouter(InputActionMap input)
        {
            _input = input;
            _input.Enable();
            Rotate = _input.FindAction(nameof(Rotate), throwIfNotFound: true);
            Acceleration = _input.FindAction(nameof(Acceleration), throwIfNotFound: true);
            FirstShoot = _input.FindAction(nameof(FirstShoot), throwIfNotFound: true);
            SecondShoot = _input.FindAction(nameof(SecondShoot), throwIfNotFound: true);
        }

        public InputAction SecondShoot { get; private set; }

        public InputAction FirstShoot { get; private set; }

        public InputAction Rotate { get; private set; }

        public InputAction Acceleration { get; private set; }

        public float Rotation => Rotate.ReadValue<float>();
        public bool Accelerate => Acceleration.phase == InputActionPhase.Performed;

        public void Dispose()
        {
            _input?.Dispose();
            SecondShoot?.Dispose();
            FirstShoot?.Dispose();
            Rotate?.Dispose();
            Acceleration?.Dispose();
        }
    }
}