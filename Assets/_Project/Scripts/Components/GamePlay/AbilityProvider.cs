using Assets._Project.Scripts.Enums;
using System;
using UnityEngine;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.Player
{
    public sealed class AbilityProvider : MonoProvider<AbilityComponent> { }

    [Serializable]
    public struct AbilityComponent
    {
        [SerializeField] private float energyPoints;
        public float EnergyPoints
        {
            get { return energyPoints; }
            set
            {
                if (value < EnergyPointsMin)
                    energyPoints = EnergyPointsMin;
                else if (value > EnergyPointsMax)
                    energyPoints = EnergyPointsMax;
                else
                    energyPoints = value;
            }
        }

        public float EnergyPointsMin;
        public float EnergyPointsMax;

        public AbilityType FirstAbilityType;
        public AbilityType SecondAbilityType;
        public AbilityType ThirdAbilityType;

        [NonSerialized] public bool AccelerationAbilityActivated;
        [NonSerialized] public bool DoubleJumpAbilityActivated;
        [NonSerialized] public bool EnergyShieldAbilityActivated;
    }
}
