using Assets._Project.Scripts.Abilities.Abstracts;
using Assets._Project.Scripts.Player.Controllers;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Assets._Project.Scripts.Abilities
{
    internal class DoubleJumpAbility : InstantAbility
    {
        private readonly CharacterController _characterController;
        private readonly PlayerMovementController _playerMovementController;

        protected DoubleJumpAbilityData DoubleJumpAbilityData
        {
            get { return (DoubleJumpAbilityData)ProlongedAbilityData; }
            set { ProlongedAbilityData = value; }
        }

        public DoubleJumpAbility(PlayerModel playerModel, DoubleJumpAbilityData doubleJumpAbilityData, CharacterController characterController, PlayerMovementController playerMovementController) : base(playerModel)
        {
            DoubleJumpAbilityData = doubleJumpAbilityData;
            _characterController = characterController;
            _playerMovementController = playerMovementController;
        }

        protected override void OnActivate()
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
                isCompleted = true;
            }
        }
    }
}
