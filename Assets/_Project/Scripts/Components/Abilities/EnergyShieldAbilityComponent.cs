using Assets._Project.Scripts.Enums;
using System;

namespace Assets._Project.Scripts.Components.Abilities
{
    [Serializable]
    public struct EnergyShieldAbilityComponent
    {
        public AbilityType AbilityType;
        public float CurrentWorkTime;
        public bool IsActive;

        public float EnergyPerSecond;
        public float EnergyTimer;
    }
}
