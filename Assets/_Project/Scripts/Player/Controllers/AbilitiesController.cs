using Assets._Project.Scripts.Abilities;
using Assets._Project.Scripts.Abilities.Abstracts;
using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.Player.Models;
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

        //[Inject]
        //private void Contract(AccelerationAbility AccelerationAbility, DoubleJumpAbility doubleJumpAbility, EnergyShieldAbility energyShieldAbility)
        //{
        //    FirstAbility = AccelerationAbility;
        //    SecondAbility = doubleJumpAbility;
        //    ThirdAbility = energyShieldAbility;
        //}

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

        private BaseAbility CreateAbility(TypeAbility ability)
        {
            switch (ability)
            {
                case TypeAbility.Acceleration:
                    return new AccelerationAbility(_playerModel);
                case TypeAbility.DoubleJump:
                    return new DoubleJumpAbility(_playerModel, _characterController, _playerMovementController);
                case TypeAbility.EnergyShield:
                    return new EnergyShieldAbility(_playerModel);
                default:
                    return null;
            }
        }
    }
}
