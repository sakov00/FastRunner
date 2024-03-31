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
            _inputActions.PC.HorizontalMove.performed += HorizontalMove;
            _inputActions.PC.HorizontalMove.canceled += HorizontalMove;
            _inputActions.PC.VerticalMove.performed += VerticalMove;
            _inputActions.PC.VerticalMove.canceled += VerticalMove;
            _inputActions.PC.Jump.performed += JumpMove;
            _inputActions.PC.Jump.canceled += JumpMove;
        }

        private void OnDisable()
        {
            _inputActions.PC.HorizontalMove.performed -= HorizontalMove;
            _inputActions.PC.HorizontalMove.canceled += HorizontalMove;
            _inputActions.PC.VerticalMove.performed -= VerticalMove;
            _inputActions.PC.VerticalMove.canceled += VerticalMove;
            _inputActions.PC.Jump.performed -= JumpMove;
            _inputActions.PC.Jump.canceled += JumpMove;
        }

        private void HorizontalMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            movementInput.x = obj.ReadValue<float>();
        }

        private void VerticalMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            movementInput.z = obj.ReadValue<float>();
        }

        private void JumpMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            movementInput.y = obj.ReadValue<float>();
        }
    }
}
