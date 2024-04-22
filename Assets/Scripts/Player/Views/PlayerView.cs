using Assets.Scripts.Player.Controllers;
using UnityEngine;

namespace Assets.Scripts.Player.Views
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerInputController playerInputController;
        private CharacterController _characterController;
        private Animator _animator;

        private static readonly string IsGrounded = "IsGrounded";
        private static readonly string IsFalling = "IsFalling";
        private static readonly string IsJump = "IsJump";
        private static readonly string InputZ = "InputZ";
        private static readonly string InputX = "InputX";

        private void Awake()
        {
            playerInputController = GetComponent<PlayerInputController>();
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        public void Move(Vector3 movement)
        {
            PlayAnimations(movement);
            _characterController.Move(movement);
        }

        public void Rotate(Quaternion quaternion)
        {
            transform.rotation = quaternion;
        }

        private void PlayAnimations(Vector3 movement)
        {
            _animator.SetBool(IsGrounded, _characterController.isGrounded);
            _animator.SetBool(IsJump, movement.y > 0);
            _animator.SetBool(IsFalling, movement.y < 0 && !_characterController.isGrounded);
            _animator.SetInteger(InputZ, (int)playerInputController.MovementInput.z);
            _animator.SetInteger(InputX, (int)playerInputController.MovementInput.x);
        }
    }
}
