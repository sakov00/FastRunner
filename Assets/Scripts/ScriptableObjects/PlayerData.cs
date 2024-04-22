using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "DefaultData/PlayerData")]

    public class PlayerData : ScriptableObject
    {
        [Range(5, 50)] public float RunningSpeed;

        [Range(5, 50)] public float RunningSpeedOnFlying;

        [Range(5, 20)] public float JumpHeight;

        [Range(-50, 0)] public float GravityValue;

        [Range(0, 45)] public float LimitRotationAngleY;

        [Range(0, 10)] public float RotationSpeedOnGround;

        [Range(0, 10)] public float RotationSpeedOnFlying;

        public TypeAbility FirstAbilityType;
        public TypeAbility SecondAbilityType;
        public TypeAbility ThirdAbilityType;
    }
}
