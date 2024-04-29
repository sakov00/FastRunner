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

        protected static DoubleJumpAbilityData AbilityData
        {
            get { return (DoubleJumpAbilityData)_instantAbilityData; }
            set { _instantAbilityData = value; }
        }

        public DoubleJumpAbility(PlayerModel playerModel, CharacterController characterController, PlayerMovementController playerMovementController)
        {
            _playerModel = playerModel;
            _characterController = characterController;
            _playerMovementController = playerMovementController;
        }

        public static void InjectData(DoubleJumpAbilityData doubleJumpAbilityData)
        {
            AbilityData = doubleJumpAbilityData;
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
