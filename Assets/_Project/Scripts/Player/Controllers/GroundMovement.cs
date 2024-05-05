using Assets._Project.InputSystem;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Assets._Project.Scripts.Player.Controllers
{
    public class GroundMovement
    {
        private PlayerModel _playerModel;
        private PlayerData _playerData;
        private IPlayerInput _playerInput;

        private Vector3 _movement;
        private Quaternion _targetRotation;

        public GroundMovement(PlayerModel playerModel, PlayerData playerData, IPlayerInput playerInput)
        {
            _playerModel = playerModel;
            _playerData = playerData;
            _playerInput = playerInput;
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
            _movement.z = _playerModel.transform.forward.z * speedValue;
            _movement.x = _playerModel.transform.forward.x * speedValue;
        }

        private void VerticalMove()
        {
            if (_playerInput.MovementInput.y != 0)
            {
                _movement.y = sqrt(_playerModel.JumpHeight * -2f * _playerModel.GravityValue);
            }
            else
            {
                _movement.y = _playerModel.GravityValue;
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
            var rotationSpeed = _playerData.RotationSpeedOnGround;
            currentDegrees += _playerInput.MovementInput.x * rotationSpeed;

            if (currentDegrees > 180f)
            {
                currentDegrees -= 360f;
            }
            float clampedRotationAngle = Mathf.Clamp(currentDegrees, -_playerData.LimitRotationAngleY, _playerData.LimitRotationAngleY);

            RaycastHit hit;
            Physics.Raycast(_playerModel.transform.position, Vector3.down, out hit);
            Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            _targetRotation = Quaternion.Euler(surfaceRotation.eulerAngles.x, clampedRotationAngle, surfaceRotation.eulerAngles.z);
            _targetRotation = Quaternion.Lerp(_playerModel.gameObject.transform.rotation, _targetRotation, _playerData.RotationSensitiveOnGround * Time.deltaTime);
        }
    }
}
