using Assets._Project.Scripts.Enums;
using System;

namespace Assets._Project.Scripts.Components.Abilities
{
    [Serializable]
    public struct DoubleJumpAbilityComponent
    {
        public AbilityType AbilityType;
        public bool CanDoubleJump;

        public float EnergyCost;
    }
}
