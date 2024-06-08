using Assets._Project.InputSystem;
using Assets._Project.Scripts.Abilities;
using Assets._Project.Scripts.Abilities.Abstracts;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Player.Controllers
{
    public class AbilitiesController : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private CharacterController _characterController;
        private PlayerMovementController _playerMovementController;
        private IPlayerInput _playerInput;

        private AccelerationAbilityData accelerationAbilityData;
        private DoubleJumpAbilityData doubleJumpAbilityData;
        private EnergyShieldAbilityData energyShieldAbilityData;

        private List<BaseAbility> Abilities { get; set; } = new List<BaseAbility>();

        [Inject]
        private void Contract(IPlayerInput playerInput, AccelerationAbilityData accelerationAbilityData,
            DoubleJumpAbilityData doubleJumpAbilityData, EnergyShieldAbilityData energyShieldAbilityData)
        {
            _playerInput = playerInput;

            this.accelerationAbilityData = accelerationAbilityData;
            this.doubleJumpAbilityData = doubleJumpAbilityData;
            this.energyShieldAbilityData = energyShieldAbilityData;
        }

        private void Awake()
        {
            _playerModel = GetComponent<PlayerModel>();
            _characterController = GetComponent<CharacterController>();
            _playerMovementController = GetComponent<PlayerMovementController>();

            Abilities.Add(new AccelerationAbility(_playerModel, accelerationAbilityData));
            Abilities.Add(new DoubleJumpAbility(_playerModel, doubleJumpAbilityData, _characterController, _playerMovementController));
            Abilities.Add(new EnergyShieldAbility(_playerModel, energyShieldAbilityData));
        }

        private void Start()
        {
            _playerInput.OnFirstAbility += Abilities.First(ability => _playerModel.FirstAbilityType == ability.AbilityData.AbilityType).Activate;
            _playerInput.OnSecondAbility += Abilities.First(ability => _playerModel.SecondAbilityType == ability.AbilityData.AbilityType).Activate;
            _playerInput.OnThirdAbility += Abilities.First(ability => _playerModel.ThirdAbilityType == ability.AbilityData.AbilityType).Activate;
        }

        private void OnDisable()
        {
            _playerInput.OnFirstAbility -= Abilities.First(ability => _playerModel.FirstAbilityType == ability.AbilityData.AbilityType).Activate;
            _playerInput.OnSecondAbility -= Abilities.First(ability => _playerModel.SecondAbilityType == ability.AbilityData.AbilityType).Activate;
            _playerInput.OnThirdAbility -= Abilities.First(ability => _playerModel.ThirdAbilityType == ability.AbilityData.AbilityType).Activate;
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < Abilities.Count; i++)
            {
                if (Abilities[i] is ProlongedAbility prolongedAbility)
                    prolongedAbility.FixedTick();
            }
        }
    }
}
