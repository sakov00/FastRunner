using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.ScriptableObjects;
using System;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Player.Models
{
    public class PlayerModel : MonoBehaviour
    {
        private PlayerData _playerData;
        [SerializeField] private float energyValue;

        [Inject]
        private void Contract(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public float RunningSpeed { get; set; }

        public float JumpHeight { get; set; }

        public bool CanDoubleJump { get; set; }

        [field: SerializeField] public float GravityValue { get; set; }

        public float EnergyValue
        {
            get
            {
                return energyValue;
            }
            set
            {
                energyValue = value < 0 ? 0 : value;
                if (energyValue == 0)
                    OnEnergyValueEnded?.Invoke();
            }
        }

        public TypeAbility FirstAbilityType { get; set; }
        public TypeAbility SecondAbilityType { get; set; }
        public TypeAbility ThirdAbilityType { get; set; }

        public event Action OnEnergyValueEnded;

        private void Awake()
        {
            SetDefaultState();
        }

        private void SetDefaultState()
        {
            RunningSpeed = _playerData.RunningSpeed;
            JumpHeight = _playerData.JumpHeight;
            GravityValue = _playerData.GravityValue;
            EnergyValue = _playerData.EnergyValue;

            FirstAbilityType = _playerData.FirstAbilityType;
            SecondAbilityType = _playerData.SecondAbilityType;
            ThirdAbilityType = _playerData.ThirdAbilityType;
        }
    }
}
