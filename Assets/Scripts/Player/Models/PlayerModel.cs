using Assets.Scripts.Enums;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Player.Models
{
    public class PlayerModel : MonoBehaviour
    {
        public PlayerData playerData;

        public float RunningSpeed { get; set; }

        public float FastRunningSpeed { get; set; }

        public float JumpHeight { get; set; }

        public bool CanDoubleJump { get; set; }

        public float GravityValue { get; set; }

        public float LimitRotationAngleY { get; set; }

        public float RotationSpeedOnGround { get; set; }

        public float RotationSpeedOnFlying { get; set; }

        public TypeAbility FirstAbilityType { get; set; }
        public TypeAbility SecondAbilityType { get; set; }
        public TypeAbility ThirdAbilityType { get; set; }

        private void Awake()
        {
            SetDafaultState();
        }

        private void SetDafaultState()
        {
            RunningSpeed = playerData.RunningSpeed;
            FastRunningSpeed = playerData.FastRunningSpeed;
            JumpHeight = playerData.JumpHeight;
            GravityValue = playerData.GravityValue;
            LimitRotationAngleY = playerData.LimitRotationAngleY;
            RotationSpeedOnGround = playerData.RotationSpeedOnGround;
            RotationSpeedOnFlying = playerData.RotationSpeedOnFlying;

            FirstAbilityType = playerData.FirstAbilityType;
            SecondAbilityType = playerData.SecondAbilityType;
            ThirdAbilityType = playerData.ThirdAbilityType;
        }
    }
}
