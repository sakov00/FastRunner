using UnityEngine;

namespace Assets.Scripts.Player.Views
{
    public class PlayerView : MonoBehaviour
    {
        private Animator _animator;
        private CharacterController _characterController;

        private static readonly string IsRun = "IsRun";
        private static readonly string IsGrounded = "IsGrounded";
        private static readonly string Gravity = "Gravity";

        private void Awake()
        {
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
            _animator.SetBool(IsRun, movement.x != 0 || movement.z != 0);
            _animator.SetBool(IsGrounded, _characterController.isGrounded);
            _animator.SetFloat(Gravity, movement.y / Time.deltaTime);
        }
    }
}
