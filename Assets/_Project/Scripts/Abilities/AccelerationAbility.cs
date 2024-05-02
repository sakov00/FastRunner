using Assets._Project.Scripts.Abilities.Abstracts;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
using UnityEngine;

namespace Assets._Project.Scripts.Abilities
{
    public class AccelerationAbility : InstantAbility
    {
        private readonly CharacterController _characterController;

        public AccelerationAbility(AbilityData abilityData, PlayerModel playerModel, CharacterController characterController)
        {
            _instantAbilityData = (InstantAbilityData)abilityData;
            _playerModel = playerModel;
            _characterController = characterController;
        }

        protected override bool ExecuteAbility()
        {
            _playerModel.RunningSpeed = 30;
            return true;
        }
    }
}
