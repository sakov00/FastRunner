using UnityEngine;

namespace Assets.Scripts.Player.Controllers
{
    public class PlayerInputController : MonoBehaviour
    {
        private PlayerInput _inputActions;

        public Vector3 MovementInput { get; private set; }
        public float FastRunInput { get; private set; }

        private void Awake()
        {
            _inputActions = new PlayerInput();
            _inputActions.Enable();
        }

        private void OnEnable()
        {
            _inputActions.PC.Movement.performed += Movement;
            _inputActions.PC.Movement.canceled += Movement;
            _inputActions.PC.FastRun.performed += FastRun;
            _inputActions.PC.FastRun.canceled += FastRun;
        }

        private void OnDisable()
        {
            _inputActions.PC.Movement.performed -= Movement;
            _inputActions.PC.Movement.canceled -= Movement;
            _inputActions.PC.FastRun.performed -= FastRun;
            _inputActions.PC.FastRun.canceled -= FastRun;
        }

        private void Movement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            MovementInput = obj.ReadValue<Vector3>();
        }

        private void FastRun(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            FastRunInput = obj.ReadValue<float>();
        }
    }
}
