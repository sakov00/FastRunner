using Assets._Project.Scripts.Components.Abilities.Interfaces;
using Assets._Project.Scripts.Enums;
using Assets._Project.Scripts.ScriptableObjects.AbilitiesData;

namespace Assets._Project.Scripts.Components.Abilities
{
    public struct DoubleJumpAbilityComponent : IInstantAbilityComponent
    {
        public AbilityType AbilityType { get; set; }
        public bool CanDoubleJump { get; set; }
        public DoubleJumpAbilityData DoubleJumpAbilityData { get; set; }
    }
}
