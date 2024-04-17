using Assets.Scripts.Player.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Player.Controllers
{
    public class AirMovement
    {
        private PlayerModel _playerModel;
        private PlayerInputController _playerInputController;

        private Vector3 _movement;
        private Quaternion _targetRotation;

        public AirMovement(PlayerModel playerModel, PlayerInputController playerInputController)
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
            var gravityValue = _movement.y;
            _movement = (_playerModel.transform.forward * _playerInputController.MovementInput.z * speedValue) + (_playerModel.transform.right * _playerInputController.MovementInput.x * _playerModel.RunningSpeed);
            _movement.y = gravityValue;
        }

        private void VerticalMove()
        {
            _movement.y += _playerModel.GravityValue * Time.deltaTime;
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
            var rotationSpeed = _playerModel.RotationSpeedOnFlying;
            currentDegrees += _playerInputController.MovementInput.x * rotationSpeed;

            _targetRotation = Quaternion.Euler(0, _targetRotation.eulerAngles.y, 0);
        }
    }
}
