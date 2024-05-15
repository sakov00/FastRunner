using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.ScriptableObjects;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

namespace Assets._Project.Scripts.Player.Models
{
    public class PlayerModel : MonoBehaviour, INotifyPropertyChanged
    {
        private PlayerData _playerData;

        [Inject]
        private void Contract(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public event Action OnDied; 
        public event Action OnEnergyValueEnded;
        public event PropertyChangedEventHandler PropertyChanged;

        public float RunningSpeed { get; set; }

        public float JumpHeight { get; set; }

        public bool CanDoubleJump { get; set; }

        public float GravityValue { get; set; }

        private float energyValue;
        public float EnergyValue
        {
            get { return energyValue; }
            set 
            {
                energyValue = value < 0 ? 0 : value;
                if (energyValue == 0)
                    OnEnergyValueEnded?.Invoke();
                OnPropertyChanged();
            }
        }    

        public AbilityType FirstAbilityType { get; set; }
        public AbilityType SecondAbilityType { get; set; }
        public AbilityType ThirdAbilityType { get; set; }

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

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void Die()
        {
            OnDied.Invoke();
            Destroy(transform.parent.gameObject);
        }
    }
}
