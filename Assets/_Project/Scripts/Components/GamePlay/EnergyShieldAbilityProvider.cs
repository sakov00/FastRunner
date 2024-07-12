using Assets._Project.Scripts.Enums;
using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public class EnergyShieldAbilityProvider : MonoProvider<EnergyShieldAbilityComponent> { }

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
