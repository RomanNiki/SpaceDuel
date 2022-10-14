using System;
using UnityEngine.InputSystem;

namespace Input
{
    public class SecondPlayerInput //: IInput//, IDisposable
    {
        /*private readonly PlayerInput.SecondPlayerActions _input;
        
        public SecondPlayerInput()
        {
            _input = new PlayerInput().SecondPlayer;
            _input.Enable();

            _input.FirstShoot.performed += FirstShootPerformed;
            _input.SecondShoot.performed += SecondShootPerformed;
        }

        public float Rotate()
        {
            return _input.Rotate.ReadValue<float>();
        }

        public bool Acceleration()
        {
            return _input.Acceleration.phase == InputActionPhase.Performed;
        }

        private void FirstShootPerformed(InputAction.CallbackContext obj)
        {
            FirstShoot?.Invoke();
        }  
        
        private void SecondShootPerformed(InputAction.CallbackContext obj)
        {
            SecondShoot?.Invoke();
        }

        public Action FirstShoot { get; set; }
        public Action SecondShoot { get; set; }

        public void Dispose()
        {
            _input.FirstShoot.performed -= FirstShootPerformed;
            _input.SecondShoot.performed -= SecondShootPerformed;
            _input.Disable();
        }*/
    }
}