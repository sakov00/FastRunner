using Assets.Scripts.Abilities;
using Assets.Scripts.Enums;
using Assets.Scripts.Player.Models;
using UnityEngine;

namespace Assets.Scripts.Player.Controllers
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

            _playerInputController.OnFirstAbility += FirstAbility.StartAbility;
            _playerInputController.OnSecondAbility += SecondAbility.StartAbility;
            _playerInputController.OnThirdAbility += ThirdAbility.StartAbility;
        }

        private BaseAbility CreateAbility(TypeAbility typeAbility)
        {
            switch (typeAbility)
            {
                case TypeAbility.Acceleration:
                    return new AccelerationAbility(_playerModel, GetComponent<CharacterController>());
                case TypeAbility.DoubleJump:
                    return new DoubleJumpAbility(_playerModel, _characterController, _playerMovementController);
                default:
                    return null;
            }
        }
    }
}
