using Assets._Project.InputSystem;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Player.Views
{
    public class PlayerView : MonoBehaviour
    {
        private IPlayerInput _playerInput;
        private CharacterController _characterController;
        private Animator _animator;

        private static readonly string IsGrounded = "IsGrounded";
        private static readonly string IsFalling = "IsFalling";
        private static readonly string IsJump = "IsJump";
        private static readonly string InputZ = "InputZ";
        private static readonly string InputX = "InputX";

        [Inject]
        private void Contract(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        public void Move(Vector3 movement)
        {
            PlayAnimations(movement);
            //PlaySounds();
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
            _animator.SetFloat(InputZ, _playerInput.MovementInput.z);
            _animator.SetFloat(InputX, _playerInput.MovementInput.x);
        }

        //private void PlaySounds()
        //{
        //    if (_animator.GetBool(IsGrounded))
        //        playerManagerSounds.PlaySteps();
        //    else
        //        playerManagerSounds.StopSteps();
        //}
    }
}
