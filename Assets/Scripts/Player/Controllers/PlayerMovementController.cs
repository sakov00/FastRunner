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
            movement.x = movementInput.x * _playerModel.RunSpeed;
            movement.z = movementInput.z * _playerModel.RunSpeed;
            if (_characterController.isGrounded && movementInput.y != 0)
            {
                movement.y = sqrt(_playerModel.JumpHeight * -2f * _playerModel.GravityValue);
            }
            else if (_characterController.isGrounded)
            {
                movement.y = 0;
            }
            else if (!_characterController.isGrounded)
            {
                movement.y += _playerModel.GravityValue * Time.deltaTime;
            }
            _playerView.Move(movement * Time.deltaTime);
        }
    }
}
