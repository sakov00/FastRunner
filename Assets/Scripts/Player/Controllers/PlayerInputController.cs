using System;
using UnityEngine;

namespace Assets.Scripts.Player.Controllers
{
    public class PlayerInputController : MonoBehaviour
    {
        private PlayerInput _inputActions;

        private Vector3 movementInput;

        public event Action<Vector3> OnMovementEvent;

        private void Awake()
        {
            _inputActions = new PlayerInput();
            _inputActions.Enable();
        }

        private void Update()
        {
            OnMovementEvent?.Invoke(movementInput);
        }

        private void OnEnable()
        {
            _inputActions.PC.Movement.performed += Movement;
            _inputActions.PC.Movement.canceled += Movement;
        }

        private void OnDisable()
        {
            _inputActions.PC.Movement.performed -= Movement;
            _inputActions.PC.Movement.canceled -= Movement;
        }

        private void Movement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            movementInput = obj.ReadValue<Vector3>();
        }
    }
}
