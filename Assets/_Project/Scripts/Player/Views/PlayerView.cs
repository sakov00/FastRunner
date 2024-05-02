using Assets._Project.Scripts.Player.Controllers;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Player.Views
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerInputController _playerInputController;
        private CharacterController _characterController;
        private Animator _animator;
        private PlayerManagerSounds playerManagerSounds;

        private static readonly string IsGrounded = "IsGrounded";
        private static readonly string IsFalling = "IsFalling";
        private static readonly string IsJump = "IsJump";
        private static readonly string InputZ = "InputZ";
        private static readonly string InputX = "InputX";

        [Inject]
        private void Contract(PlayerManagerSounds playerManagerSounds, PlayerInputController playerInputController, CharacterController characterController, Animator animator)
        {
            this.playerManagerSounds = playerManagerSounds;
            _playerInputController = playerInputController;
            _characterController = characterController;
            _animator = animator;
        }

        public void Move(Vector3 movement)
        {
            PlayAnimations(movement);
            PlaySounds();
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
            _animator.SetInteger(InputZ, (int)_playerInputController.MovementInput.z);
            _animator.SetInteger(InputX, (int)_playerInputController.MovementInput.x);
        }

        private void PlaySounds()
        {
            if (_animator.GetBool(IsGrounded))
                playerManagerSounds.PlaySteps();
            else
                playerManagerSounds.StopSteps();
        }
    }
}