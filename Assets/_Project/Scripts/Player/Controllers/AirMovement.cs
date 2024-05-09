using Assets._Project.InputSystem;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets._Project.Scripts.Player.Controllers
{
    public class AirMovement
    {
        private PlayerModel _playerModel;
        private PlayerData _playerData;
        private IPlayerInput _playerInput;

        private Vector3 _movement;
        private Quaternion _targetRotation;

        public AirMovement(PlayerModel playerModel, PlayerData playerData, IPlayerInput playerInput)
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
            var gravityValue = _movement.y;
            _movement = (_playerModel.transform.forward * speedValue) + (_playerModel.transform.right * _playerInput.MovementInput.x * _playerData.RunningSpeedLeftRightOnFlying);
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
            var currentDegreesY = _targetRotation.eulerAngles.y;
            var currentDegreesZ = _targetRotation.eulerAngles.z;
            currentDegreesY += _playerInput.MovementInput.x * _playerData.RotationSpeedOnFlying;
            currentDegreesZ -= _playerInput.MovementInput.x * _playerInput.MovementInput.z * _playerData.RotationSpeedOnFlying;

            if (currentDegreesY > 180f)
            {
                currentDegreesY -= 360f;
            }
            float clampedRotationAngleY = Mathf.Clamp(currentDegreesY, -_playerData.LimitRotationAngleY, _playerData.LimitRotationAngleY);

            if (currentDegreesZ > 180f)
            {
                currentDegreesZ -= 360f;
            }
            float clampedRotationAngleZ = Mathf.Clamp(currentDegreesZ, -_playerData.LimitRotationAngleY, _playerData.LimitRotationAngleY);

            RaycastHit hit;
            Physics.Raycast(_playerModel.transform.position, Vector3.down, out hit);
            Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            _targetRotation = Quaternion.Euler(surfaceRotation.eulerAngles.x, clampedRotationAngleY, clampedRotationAngleZ);
            _targetRotation = Quaternion.Lerp(_playerModel.gameObject.transform.rotation, _targetRotation, _playerData.RotationSensitiveOnFlying * Time.deltaTime);
        }
    }
}
