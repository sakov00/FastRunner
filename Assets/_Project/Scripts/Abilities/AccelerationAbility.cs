﻿using Assets._Project.Scripts.Abilities.Abstracts;
using Assets._Project.Scripts.Player.Models;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;
using Zenject;

namespace Assets._Project.Scripts.Abilities
{
    public class AccelerationAbility : ProlongedAbility
    {
        protected static AccelerationAbilityData AbilityData
        {
            get { return (AccelerationAbilityData)_prolongedAbilityData; }
            set { _prolongedAbilityData = value; }
        }

        public AccelerationAbility(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public static void InjectData(AccelerationAbilityData accelerationAbilityData)
        {
            AbilityData = accelerationAbilityData;
        }

        protected override void OnActivate()
        {
            _playerModel.RunningSpeed += 20;
        }

        protected override void OnDeactivate()
        {
            _playerModel.RunningSpeed -= 20;
        }
    }
}
