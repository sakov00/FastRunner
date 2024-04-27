using Assets.Scripts.Enums;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Models
{
    public class PlayerModel : MonoBehaviour
    {
        private PlayerData _playerData;

        [Inject]
        private void Contract(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public float RunningSpeed { get; set; }

        public float JumpHeight { get; set; }

        public bool CanDoubleJump { get; set; }

        public float GravityValue { get; set; }

        public TypeAbility FirstAbilityType { get; set; }
        public TypeAbility SecondAbilityType { get; set; }
        public TypeAbility ThirdAbilityType { get; set; }

        private void Awake()
        {
            SetDefaultState();
        }

        private void SetDefaultState()
        {
            RunningSpeed = _playerData.RunningSpeed;
            JumpHeight = _playerData.JumpHeight;
            GravityValue = _playerData.GravityValue;

            FirstAbilityType = _playerData.FirstAbilityType;
            SecondAbilityType = _playerData.SecondAbilityType;
            ThirdAbilityType = _playerData.ThirdAbilityType;
        }
    }
}
