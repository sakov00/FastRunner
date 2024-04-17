using Assets.Scripts.Player.Controllers;
using Assets.Scripts.Player.Models;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Assets.Scripts.Abilities
{
    internal class DoubleJumpAbility : BaseAbility
    {
        private readonly PlayerModel _playerModel;
        private readonly CharacterController _characterController;
        private readonly PlayerMovementController _playerMovementController;

        public DoubleJumpAbility(PlayerModel playerModel, CharacterController characterController, PlayerMovementController playerMovementController) 
        {
            _playerModel = playerModel;
            _characterController = characterController;
            _playerMovementController = playerMovementController;
        }

        public override void StartAbility()
        {
            if (_characterController.isGrounded)
            {
                _playerModel.CanDoubleJump = true;
            }
            if (!_characterController.isGrounded && _playerModel.CanDoubleJump)
            {
                var movement = new Vector3(0, sqrt(_playerModel.JumpHeight * -2f * _playerModel.GravityValue), 0);
                _playerMovementController.SetMovement(movement);
                _playerModel.CanDoubleJump = false;
            }
        }
    }
}
