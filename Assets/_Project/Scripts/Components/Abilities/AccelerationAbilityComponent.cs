using Assets._Project.Scripts.Enums;
using System;

namespace Assets._Project.Scripts.Components.Abilities
{
    [Serializable]
    public struct AccelerationAbilityComponent
    {
        public AbilityType AbilityType;
        [NonSerialized] public float CurrentWorkTime;
        [NonSerialized] public bool IsActive;

        public float EnergyPerSecond;
        public float EnergyTimer;

        public float ValueSpeedUp;
        public float ValueSpeedDown;
    }
}
