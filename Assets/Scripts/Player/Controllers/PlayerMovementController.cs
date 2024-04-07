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

        private void Update()
        {
            HorizontalMove(_playerInputController.MovementInput, _playerInputController.FastRunInput);
            VerticalMove(_playerInputController.MovementInput);
            Rotate(_playerInputController.MovementInput);

            _playerView.Move(movement * Time.deltaTime);
            _playerView.Rotate(Quaternion.Lerp(transform.rotation, targetRotation, 2 * Time.deltaTime));
        }

        private void HorizontalMove(Vector3 movementInput, float fastRunInput)
        {
            var speedValue = fastRunInput == 1 ? _playerModel.FastRunningSpeed : _playerModel.RunningSpeed;
            movement.z = transform.forward.z * movementInput.z * speedValue;
            movement.x = transform.forward.x * movementInput.z * speedValue;
        }

        private void VerticalMove(Vector3 movementInput)
        {

            if (_characterController.isGrounded && movementInput.y != 0)
            {
                movement.y = sqrt(_playerModel.JumpHeight * -2f * _playerModel.GravityValue);
            }
            else
            {
                movement.y += _playerModel.GravityValue * Time.deltaTime;
            }
        }

        private void Rotate(Vector3 movementInput) 
        {
            var currentDegrees = targetRotation.eulerAngles.y;
            var rotationSpeed = _characterController.isGrounded ? _playerModel.RotationSpeedOnGround : _playerModel.RotationSpeedOnFlying;
            currentDegrees += movementInput.x * rotationSpeed * Time.deltaTime;
            //currentDegrees = clamp(currentDegrees, -_playerModel.LimitRotationAngleY, _playerModel.LimitRotationAngleY); 

            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit);
            Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            targetRotation = Quaternion.Euler(surfaceRotation.eulerAngles.x, currentDegrees, surfaceRotation.eulerAngles.z);
        }
    }
}
