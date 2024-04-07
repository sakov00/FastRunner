using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "DefaultData/PlayerData")]

    public class PlayerData : ScriptableObject
    {
        public float RunningSpeed;

        public float FastRunningSpeed;

        public float JumpHeight;

        public float GravityValue;

        public float LimitRotationAngleY;

        public float RotationSpeedOnGround;

        public float RotationSpeedOnFlying;
    }
}
