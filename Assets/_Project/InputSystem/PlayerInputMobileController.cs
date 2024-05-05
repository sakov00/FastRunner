using System;
using UnityEngine;

namespace Assets._Project.InputSystem
{
    public class PlayerInputMobileController : IPlayerInput
    {
        private PlayerInput _inputActions;

        public Vector3 MovementInput { get; set; }
        public event Action OnFirstAbility;
        public event Action OnSecondAbility;
        public event Action OnThirdAbility;

        public PlayerInputMobileController()
        {
            _inputActions = new PlayerInput();
            _inputActions.Enable();
            OnEnable();
        }

        public void OnEnable()
        {
            _inputActions.Mobile.Movement.performed += Movement;
            _inputActions.Mobile.Movement.canceled += Movement;
            _inputActions.Mobile.Jump.performed += Jump;
            _inputActions.Mobile.Jump.canceled += Jump;
            _inputActions.Mobile.FirstAbility.performed += FirstAbility;
            _inputActions.Mobile.SecondAbility.performed += SecondAbility;
            _inputActions.Mobile.ThirdAbility.performed += ThirdAbility;
        }

        public void OnDisable()
        {
            _inputActions.Mobile.Movement.performed -= Movement;
            _inputActions.Mobile.Movement.canceled -= Movement;
            _inputActions.Mobile.Jump.performed -= Jump;
            _inputActions.Mobile.Jump.canceled -= Jump;
            _inputActions.Mobile.FirstAbility.performed -= FirstAbility;
            _inputActions.Mobile.SecondAbility.performed -= SecondAbility;
            _inputActions.Mobile.ThirdAbility.performed -= ThirdAbility;
        }

        private void Movement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            var movement = obj.ReadValue<Vector2>();
            MovementInput = new Vector3(movement.x, 0, movement.y);
        }

        private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            var movementY = obj.ReadValue<float>();
            MovementInput = new Vector3(0, movementY, 0);
        }

        private void FirstAbility(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnFirstAbility?.Invoke();
        }

        private void SecondAbility(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnSecondAbility?.Invoke();
        }

        private void ThirdAbility(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnThirdAbility?.Invoke();
        }
    }
}
