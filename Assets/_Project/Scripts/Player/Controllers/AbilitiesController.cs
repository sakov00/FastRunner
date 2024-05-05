using Assets._Project.InputSystem;
using Assets._Project.Scripts.Abilities;
using Assets._Project.Scripts.Abilities.Abstracts;
using Assets._Project.Scripts.Player.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Player.Controllers
{
    public class AbilitiesController : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private IPlayerInput _playerInput;

        private List<BaseAbility> Abilities { get; set; } = new List<BaseAbility>();

        [Inject]
        private void Contract(PlayerModel playerModel, IPlayerInput playerInput, AccelerationAbility accelerationAbility, DoubleJumpAbility doubleJumpAbility, EnergyShieldAbility energyShieldAbility)
        {
            _playerModel = playerModel;
            _playerInput = playerInput;

            Abilities.Add(accelerationAbility);
            Abilities.Add(doubleJumpAbility);
            Abilities.Add(energyShieldAbility);
        }

        private void Start()
        {
            _playerInput.OnFirstAbility += Abilities.First(ability => _playerModel.FirstAbilityType == ability.AbilityData.AbilityType).Activate;
            _playerInput.OnSecondAbility += Abilities.First(ability => _playerModel.SecondAbilityType == ability.AbilityData.AbilityType).Activate;
            _playerInput.OnThirdAbility += Abilities.First(ability => _playerModel.ThirdAbilityType == ability.AbilityData.AbilityType).Activate;
        }
    }
}
