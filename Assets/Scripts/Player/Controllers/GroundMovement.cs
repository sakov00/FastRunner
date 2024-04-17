using Assets.Scripts.Player.Models;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Assets.Scripts.Player.Controllers
{
    public class GroundMovement
    {
        private PlayerModel _playerModel;
        private PlayerInputController _playerInputController;

        private Vector3 _movement;
        private Quaternion _targetRotation;

        public GroundMovement(PlayerModel playerModel, PlayerInputController playerInputController)
        {
            _playerModel = playerModel;
            _playerInputController = playerInputController;
        }

        public Vector3 Move(Vector3 movement)
        {
            _movement = movement;
            HorizontalMove();
            VerticalMove();
            return _movement;
        }

        private void HorizontalMove()
        {
            var speedValue = _playerInputController.FastRunInput == 1 ? _playerModel.FastRunningSpeed : _playerModel.RunningSpeed;
            _movement.z = _playerModel.transform.forward.z * _playerInputController.MovementInput.z * speedValue;
            _movement.x = _playerModel.transform.forward.x * _playerInputController.MovementInput.z * speedValue;
        }

        private void VerticalMove()
        {
            float movementDirectionY = _movement.y;
            if (_playerInputController.MovementInput.y != 0)
            {
                _movement.y = sqrt(_playerModel.JumpHeight * -2f * _playerModel.GravityValue);
            }
            else
            {
                _movement.y = movementDirectionY;
            }
        }

        public Quaternion Rotate(Quaternion rotation)
        {
            _targetRotation = rotation;
            CalculateRotate();
            return _targetRotation;
        }

        private void CalculateRotate()
        {
            var currentDegrees = _targetRotation.eulerAngles.y;
            var rotationSpeed = _playerModel.RotationSpeedOnGround;
            currentDegrees += _playerInputController.MovementInput.x * rotationSpeed;

            RaycastHit hit;
            Physics.Raycast(_playerModel.transform.position, Vector3.down, out hit);
            Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            _targetRotation = Quaternion.Euler(surfaceRotation.eulerAngles.x, currentDegrees, surfaceRotation.eulerAngles.z);
        }
    }
}
