using Assets._Project.Scripts.Enums;
using System;
using Voody.UniLeo;

namespace Assets._Project.Scripts.Components.GamePlay
{
    public class DoubleJumpAbilityProvider : MonoProvider<DoubleJumpAbilityComponent> { }

    [Serializable]
    public struct DoubleJumpAbilityComponent
    {
        public AbilityType AbilityType;
        public bool CanDoubleJump;

        public float EnergyCost;
    }
}
