using System;
using UnityEngine;

namespace Assets._Project.InputSystem
{
    public class PlayerInputPCController : IPlayerInput
    {
        private PlayerInput _inputActions;

        public Vector3 MovementInput { get; set; }
        public event Action OnFirstAbility;
        public event Action OnSecondAbility;
        public event Action OnThirdAbility;

        public PlayerInputPCController()
        {
            _inputActions = new PlayerInput();
            _inputActions.Enable();
            OnEnable();
        }

        public void OnEnable()
        {
            _inputActions.PC.Movement.performed += Movement;
            _inputActions.PC.Movement.canceled += Movement;
            _inputActions.PC.FirstAbility.performed += FirstAbility;
            _inputActions.PC.SecondAbility.performed += SecondAbility;
            _inputActions.PC.ThirdAbility.performed += ThirdAbility;
        }

        public void OnDisable()
        {
            _inputActions.PC.Movement.performed -= Movement;
            _inputActions.PC.Movement.canceled -= Movement;
            _inputActions.PC.FirstAbility.performed -= FirstAbility;
            _inputActions.PC.SecondAbility.performed -= SecondAbility;
            _inputActions.PC.ThirdAbility.performed -= ThirdAbility;
        }

        private void Movement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            MovementInput = obj.ReadValue<Vector3>();
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
