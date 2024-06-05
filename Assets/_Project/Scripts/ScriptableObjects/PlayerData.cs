using Assets._Project.Scripts.Enums;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "DefaultData/PlayerData")]

    public class PlayerData : ScriptableObject
    {
        [Header("Health"), Space(10)]

        [Range(0, 100)] public float HealthPointsMax;

        [Range(0, 100)] public float HealthPointsMin;

        [Range(0, 100)] public float EnergyPoints;

        [Header("Damage"), Space(10)]

        [Range(0, 5)] public float DamageCoolDown;

        [Header("Energy"), Space(10)]

        [Range(0, 100)] public float EnergyPointsMax;

        [Range(0, 100)] public float EnergyPointsMin;

        [Range(0, 100)] public float HealthPoints;

        [Header("Movement"), Space(10)]

        [Range(5, 100)] public float RunningSpeed;

        [Range(1, 100)] public float RunningSpeedLeftRightOnFlying;

        [Range(5, 20)] public float JumpHeight;

        [Range(-50, 0)] public float GravityValue;

        [Header("Rotation"), Space(10)]

        [Range(0, 90)] public float LimitRotationAngleY;

        [Range(0, 20)] public float RotationSpeedOnGround;

        [Range(0, 20)] public float RotationSensitiveOnGround;

        [Range(0, 20)] public float RotationSpeedOnFlying;

        [Range(0, 20)] public float RotationSensitiveOnFlying;

        [Header("Abilities"), Space(10)]

        public AbilityType FirstAbilityType;

        public AbilityType SecondAbilityType;

        public AbilityType ThirdAbilityType;
    }
}
