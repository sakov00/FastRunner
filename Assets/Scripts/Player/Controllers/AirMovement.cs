using Assets.Scripts.Player.Models;
using UnityEngine;

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
            var speedValue = _playerModel.RunningSpeed;
            var gravityValue = _movement.y;
            _movement = (_playerModel.transform.forward * speedValue) + (_playerModel.transform.right * _playerInputController.MovementInput.x * _playerModel.RunningSpeedOnFlying);
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

            if (currentDegrees > 180f)
            {
                currentDegrees -= 360f;
            }
            float clampedRotationAngle = Mathf.Clamp(currentDegrees, -_playerModel.LimitRotationAngleY, _playerModel.LimitRotationAngleY);

            RaycastHit hit;
            Physics.Raycast(_playerModel.transform.position, Vector3.down, out hit);
            Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            _targetRotation = Quaternion.Euler(surfaceRotation.eulerAngles.x, clampedRotationAngle, surfaceRotation.eulerAngles.z);
        }
    }
}
