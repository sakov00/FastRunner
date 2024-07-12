using Assets._Project.Scripts.Enums;
using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public class AccelerationAbilityProvider : MonoProvider<AccelerationAbilityComponent> { }

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
