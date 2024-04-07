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

        public float GravityValue { get; set; }

        public float LimitRotationAngleY { get; set; }

        public float RotationSpeedOnGround { get; set; }

        public float RotationSpeedOnFlying { get; set; }

        private void Awake()
        {
            RunningSpeed = playerData.RunningSpeed;
            FastRunningSpeed = playerData.FastRunningSpeed;
            JumpHeight = playerData.JumpHeight;
            GravityValue = playerData.GravityValue;
            LimitRotationAngleY = playerData.LimitRotationAngleY;
            RotationSpeedOnGround = playerData.RotationSpeedOnGround;
            RotationSpeedOnFlying = playerData.RotationSpeedOnFlying;
        }
    }
}
