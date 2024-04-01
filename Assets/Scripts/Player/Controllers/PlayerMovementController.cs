using Assets.Scripts.Player.Models;
using Assets.Scripts.Player.Views;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Assets.Scripts.Player.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private PlayerInputController _playerInputController;
        private PlayerView _playerView;
        private CharacterController _characterController;

        private Vector3 movement;
        private Quaternion targetRotation;

        private void Awake()
        {
            _playerModel = GetComponent<PlayerModel>();
            _playerInputController = GetComponent<PlayerInputController>();
            _playerView = GetComponent<PlayerView>();
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            _playerInputController.OnMovementEvent += Move;
        }

        private void OnDisable()
        {
            _playerInputController.OnMovementEvent -= Move;
        }

        private void Move(Vector3 movementInput)
        {
            movement.z = transform.forward.z * movementInput.z * _playerModel.RunSpeed;
            movement.x = transform.forward.x * movementInput.z * _playerModel.RunSpeed;
            if (movementInput.x == -1)
            {
                targetRotation = Quaternion.Euler(0f, -15, 0f);
            }
            else if (movementInput.x == 1)
            {
                targetRotation = Quaternion.Euler(0f, 15, 0f);
            }
            else
            {
                targetRotation = Quaternion.identity;
            }

            if (_characterController.isGrounded)
            {
                if (movementInput.y != 0)
                {
                    movement.y = sqrt(_playerModel.JumpHeight * -2f * _playerModel.GravityValue);
                }
                else
                {
                    movement.y = 0;
                }
            }
            else
            {
                movement.y += _playerModel.GravityValue * Time.deltaTime;
            }

            _playerView.Move(movement * Time.deltaTime);
            _playerView.Rotate(Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime));
        }
    }
}
