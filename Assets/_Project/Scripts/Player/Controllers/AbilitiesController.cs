using Assets._Project.Scripts.Abilities;
using Assets._Project.Scripts.Abilities.Abstracts;
using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
using UnityEngine;

namespace Assets._Project.Scripts.Player.Controllers
{
    public class AbilitiesController : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private PlayerInputController _playerInputController;
        private CharacterController _characterController;
        private PlayerMovementController _playerMovementController;

        private BaseAbility FirstAbility { get; set; }
        private BaseAbility SecondAbility { get; set; }
        private BaseAbility ThirdAbility { get; set; }

        private void Start()
        {
            _playerModel = GetComponent<PlayerModel>();
            _playerInputController = GetComponent<PlayerInputController>();
            _characterController = GetComponent<CharacterController>();
            _playerMovementController = GetComponent<PlayerMovementController>();

            FirstAbility = CreateAbility(_playerModel.FirstAbilityType);
            SecondAbility = CreateAbility(_playerModel.SecondAbilityType);
            ThirdAbility = CreateAbility(_playerModel.ThirdAbilityType);

            _playerInputController.OnFirstAbility += FirstAbility.Activate;
            _playerInputController.OnSecondAbility += SecondAbility.Activate;
            _playerInputController.OnThirdAbility += ThirdAbility.Activate;
        }

        private BaseAbility CreateAbility(AbilityData ability)
        {
            switch (ability.AbilityType)
            {
                case TypeAbility.Acceleration:
                    return new AccelerationAbility(ability, _playerModel, _characterController);
                case TypeAbility.DoubleJump:
                    return new DoubleJumpAbility(ability, _playerModel, _characterController, _playerMovementController);
                default:
                    return null;
            }
        }
    }
}
